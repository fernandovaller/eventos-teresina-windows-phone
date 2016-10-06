using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ExemploServices.Resources;
using System.Xml.Linq;
using System.Runtime.Serialization.Json;
using System.Windows.Media.Imaging;

namespace ExemploServices
{
    public partial class MainPage : PhoneApplicationPage
    {
        private List<ResultadoEventos> Eventoss;
        private List<Eventos> listaEventoDB;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            //baixa os eventos do webservice
            webserviceEventosXML();

            //Carrega os registro do DB para uma lista
            carregarEventosList();
        }


        //Baixar evento do webservice
        public void webserviceEventosXML()
        {
            WebClient request = new WebClient();
            request.OpenReadCompleted += resp_OpenReadCompleted;
                     
            //Exemplo de um webservice com saida em XML
            Uri uri = new Uri("http://fernandovaller.com/eventos.xml");

            request.OpenReadAsync(uri);    
        }

        void resp_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {

            XDocument doc = XDocument.Load(e.Result);

            if (doc != null)
            {
                Eventoss = new List<ResultadoEventos>();

                //Salvar os eventos no DB
                using (var db = new EventosDataContext())
                {                    

                    //lendo os registro do xml
                    foreach (var item in doc.Descendants("evento"))
                    {
                        string nomeXML = (string)item.Element("nome");
                        string dataXML = (string)item.Element("data");
                        string destaqueXML = (string)item.Element("destaque");
                        string imageXML = (string)item.Element("image");

                        //Verifica se já existe registro com esse nome
                        if(!listaEventoDB.Any(l => l.Nome == nomeXML))
                        {
                            //Caso não existe inseri
                            Eventos novoEvento = new Eventos
                            {
                                Nome = nomeXML,
                                Data = dataXML,
                                Destaque = destaqueXML,
                                Image = imageXML
                            };

                            db.Eventos.InsertOnSubmit(novoEvento);
                        }                        
                    }
                    
                    //Salva as inserções
                    db.SubmitChanges();
                }
            }                                              

            //Carrega os evento do DB no list
            carregarEventosDB();

        }

        void CarregarImagem(string nome, int tamanho)
        {
            Uri uri = new Uri("/" + nome + ".jpg", UriKind.Relative);
            BitmapImage bmp = new BitmapImage(uri);
            //slideshow.Source = bmp;
            //slideshow.Width = tamanho;
        }

        private void btMais_Click(object sender, RoutedEventArgs e)
        {

            Button btn = (Button)sender;
            Eventos res = (Eventos)btn.DataContext;

            MessageBox.Show(res.Destaque);                    

        }


        //Carregar evento do DB
        void carregarEventosDB()
        {
            using (var db = new EventosDataContext())
            {
                var resultado = (from evento in db.Eventos
                                 orderby evento.Nome
                                 select evento).ToList();    
                //evita duplicados
                lstContatos.ItemsSource = resultado.Distinct().ToList();
            }
        }

        //Carrega os eventos em uma lista para verificar
        //se já existe no banco de dados e nao duplicar no db
        void carregarEventosList()
        {
            listaEventoDB = new List<Eventos>();

            using (var db = new EventosDataContext())
            {
                var resultado = (from evento in db.Eventos
                                 orderby evento.Nome
                                 select evento).ToList();
                //evita duplicados
                //lstContatos.ItemsSource = resultado.Distinct().ToList();
                listaEventoDB = resultado;
            }
        }

        private void lstContatos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            Eventos selectEvent = (sender as ListBox).SelectedItem as Eventos;
            
            if (selectEvent != null)
            {                
                NavigationService.Navigate(new Uri("/Page2.xaml?id=" + selectEvent.Id, UriKind.Relative));    
            }
        }


    }
}
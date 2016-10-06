using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media.Imaging;

namespace ExemploServices
{
    public partial class Page2 : PhoneApplicationPage
    {

        Eventos eventoSelect = new Eventos();
        string pathImageServer = "http://ingressosky.com.br/uploads/eventos/";

        public Page2()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //base.OnNavigatedTo(e);                   

            string id = "";
            
            //Evita erro se parametro nao existir
            if (NavigationContext.QueryString.TryGetValue("id", out id))
            {
                               
                getEvento(id);

                if (eventoSelect != null)
                {                    

                    txtEvento.Text = eventoSelect.Nome;
                    txtDestaque.Text = eventoSelect.Destaque;
                    string img = eventoSelect.Image;

                    if (img != null)
                    {
                        carregarImagem(pathImageServer + img, imgDestaque);
                    }
                }
                    
            }

        }

        //consulta o evento no DB pelo ID
        private void getEvento(string Id)
        {
            //listaEventoDB = new List<Eventos>();

            using (var db = new EventosDataContext())
            {
                var resultado = (from evento in db.Eventos
                                 where evento.Id.Equals(Id)
                                 select evento).FirstOrDefault();


                eventoSelect = resultado;

            }

        }

        //Carrega a imagem desejada
        private void carregarImagem(string url, Image destino)
        {
            LoadingImage.Visibility = Visibility.Visible;

            Uri myUri = new Uri(url, UriKind.Absolute);
            BitmapImage bmi = new BitmapImage();
            bmi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            bmi.UriSource = myUri;

            destino.Source = bmi;
        }

        //retorna Uri
        private Uri carregarImagem(string url)
        {
            LoadingImage.Visibility = Visibility.Visible;

            Uri myUri = new Uri(url, UriKind.Absolute);
            return myUri;            
        }

        //Quando terminar de carregar a imagem
        private void KittyPic_ImageOpened(object sender, RoutedEventArgs e)
        {
            LoadingImage.Visibility = Visibility.Collapsed;
            LoadingImage.Height = 0;
        }

        //Se nao conseguir carregar a imagem 
        private void KittyPic_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            Image img = (Image)sender;
            img.Source = carregarDefault();
        }

        //Carrega a imagem padrão
        private BitmapImage carregarDefault()
        {
            Uri myUri = new Uri("/Assets/no-img.png", UriKind.Relative);
            BitmapImage bmi = new BitmapImage();
            bmi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            bmi.UriSource = myUri;

            return bmi;
        }

        private void btnTite_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Criar atalho com title");
        }


        //Criar o tile para o evento
        private void btTile_Click(object sender, RoutedEventArgs e)
        {
            StandardTileData dadosTile = new StandardTileData
            {
                //Frente
                Title = "Evento",
                BackgroundImage = carregarImagem(pathImageServer + eventoSelect.Image),

                //Verso
                BackContent = eventoSelect.Nome,
                BackTitle = eventoSelect.Data,                
            };

            //Uri para o evento
            Uri uri = new Uri("/Page2.xaml?id="+eventoSelect.Id, UriKind.Relative);

            //Pesquisa para ver se já existe
            ShellTile tileEncontrato = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri == uri);

            if(tileEncontrato != null)
            {
                MessageBox.Show("Já existe um Tile para esse evento");
            }
            else
            {
                ShellTile.Create(uri, dadosTile);
            }  
        }
    }
}
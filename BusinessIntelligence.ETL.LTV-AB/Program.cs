using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessIntelligence.Data;
namespace BusinessIntelligence.ETL.LTV_AB
{
    class Program
    {
        static void Main(string[] args)
        {
            var qex = new QueryExecutor(Connections.GetNewConnection(Database.REDSHIFT));
            LTVTest.QueryExecutor = qex;
            
               LTVTest.RefreshPurchaseOrder();


            LTVTest LTVKinoplex = new LTVTest("LTV Kinoplex");

            LTVKinoplex.LTVTestGroupList.Add(new LTVTestGroup("Compradores Kinoplex", ".LTVKinoplex-A.txt"));
            LTVKinoplex.LTVTestGroupList.Add(new LTVTestGroup("Outros compradores Cinema", ".LTVKinoplex-B.txt"));

            LTVKinoplex.Run();

            LTVTest LTVCocoBambu = new LTVTest("LTV Coco Bambu");

            LTVCocoBambu.LTVTestGroupList.Add(new LTVTestGroup("Compradores Coco Bambu", ".LTVCocoBambu-A.txt"));
            LTVCocoBambu.LTVTestGroupList.Add(new LTVTestGroup("Outros compradores gastronomia", ".LTVCocoBambu-B.txt"));

            LTVCocoBambu.Run();

            LTVTest LTVProdutos = new LTVTest("LTV Produtos");

            LTVProdutos.LTVTestGroupList.Add(new LTVTestGroup("Produtos - VP Facebook", ".LTVProdutos-A.txt"));
            LTVProdutos.LTVTestGroupList.Add(new LTVTestGroup("Produtos - Outros VPs", ".LTVProdutos-B.txt"));
            LTVProdutos.LTVTestGroupList.Add(new LTVTestGroup("Produtos sem VP", ".LTVProdutos-C.txt"));

            LTVProdutos.Run();

            LTVTest LTVLocal = new LTVTest("LTV Local");

            LTVLocal.LTVTestGroupList.Add(new LTVTestGroup("Local - VP Facebook", ".LTVLocal-A.txt"));
            LTVLocal.LTVTestGroupList.Add(new LTVTestGroup("Local - Outros VPs", ".LTVLocal-B.txt"));
            LTVLocal.LTVTestGroupList.Add(new LTVTestGroup("Local sem VP", ".LTVLocal-C.txt"));

            LTVLocal.Run();

            LTVTest LTVViagens = new LTVTest("LTV Viagens");

            LTVViagens.LTVTestGroupList.Add(new LTVTestGroup("Viagens - VP Facebook", ".LTVViagens-A.txt"));
            LTVViagens.LTVTestGroupList.Add(new LTVTestGroup("Viagens - Outros VPs", ".LTVViagens-B.txt"));
            LTVViagens.LTVTestGroupList.Add(new LTVTestGroup("Viagens sem VP", ".LTVViagens-C.txt"));

            LTVViagens.Run();

            LTVTest LTVIphone = new LTVTest("LTV Compradores iPhone");

            LTVIphone.LTVTestGroupList.Add(new LTVTestGroup("Compradores iPhone", ".LTVCompradoresIPhone-A.txt"));
            LTVIphone.LTVTestGroupList.Add(new LTVTestGroup("Outros compradores de produto", ".LTVCompradoresIPhone-B.txt"));
            LTVIphone.LTVTestGroupList.Add(new LTVTestGroup("Compradores de viagem", ".LTVCompradoresIPhone-C.txt"));

            LTVIphone.Run();
            
          


            LTVTest LTVCinema = new LTVTest("LTV Cinema");

            LTVCinema.LTVTestGroupList.Add(new LTVTestGroup("Compradores de cinema", ".LTVCinema-A.txt"));
            LTVCinema.LTVTestGroupList.Add(new LTVTestGroup("Outros compradores local", ".LTVCinema-B.txt"));
           
            LTVCinema.Run();

            LTVTest LTVVitoria = new LTVTest("LTV Vitoria");

            LTVVitoria.LTVTestGroupList.Add(new LTVTestGroup("Ativação em Vitória", ".LTVVitoria-A.txt"));
            LTVVitoria.LTVTestGroupList.Add(new LTVTestGroup("Ativação em outras cidades", ".LTVVitoria-B.txt"));

            LTVVitoria.Run();



    
            LTVTest LTVFlyersCinemaSP = new LTVTest("LTV Flyers de cinema SP");

            LTVFlyersCinemaSP.LTVTestGroupList.Add(new LTVTestGroup("Compradores flyers cinema", ".LTVFlyersCinema-A.txt"));
            LTVFlyersCinemaSP.LTVTestGroupList.Add(new LTVTestGroup("Outros compradores cinema", ".LTVFlyersCinema-B.txt"));

            LTVFlyersCinemaSP.Run();

            LTVTest LTVCinema1RS = new LTVTest("LTV Cinema 1R$");

            LTVCinema1RS.LTVTestGroupList.Add(new LTVTestGroup("Compradores cinema 1R$", ".LTVCinema1RS-A.txt"));
            LTVCinema1RS.LTVTestGroupList.Add(new LTVTestGroup("Outros compradores cinema", ".LTVCinema1RS-B.txt"));
            LTVCinema1RS.Run();

            BusinessIntelligence.Util.Log.GetInstance().WriteLine("FIM DO PROCESSO!");
            Console.ReadLine();
        }
    }
}

using System; // bunu her zaman eklememiz lazim
using System.IO; //StreamReader ve StreamWriter siniflari için
using System.Net.Sockets; // Socket, TcpListener ve NetworkStrem siniflari için

public class Server
{

    public static void Main()
    {

        //Bilgi alisverisi için bilgi almak istedigimiz port numarasini TcpListener sinifi ile gerçeklestiriyoruz

        TcpListener TcpDinleyicisi = new TcpListener(1234);
        TcpDinleyicisi.Start();

        Console.WriteLine("Sunucu baslatildi...");

        
        Socket IstemciSoketi = TcpDinleyicisi.AcceptSocket();


      
        if (!IstemciSoketi.Connected)
        {
            Console.WriteLine("Sunucu baslatilamiyor...");
        }
        else
        {
           
            while (true)
            {
                Console.WriteLine("Istemci baglantisi saglandi...");

                //IstemciSoketi verilerini NetworkStream sinifi türünden nesneye aktariyoruz.
                NetworkStream AgAkimi = new NetworkStream(IstemciSoketi);
                //Soketteki bilgilerle islem yapabilmek için StreamReader ve StreamWriter siniflarini kullaniyoruz
                StreamWriter AkimYazici = new StreamWriter(AgAkimi);
                StreamReader AkimOkuyucu = new StreamReader(AgAkimi);


               
                try
                {
                    string IstemciString = AkimOkuyucu.ReadLine();

                    Console.WriteLine("Gelen Bilgi:" + IstemciString);

                    AkimYazici.Flush();
                }

                catch
                {
                    Console.WriteLine("Sunucu kapatiliyor...");
                    return;
                }
            }
        }

        IstemciSoketi.Close();
        Console.WriteLine("Sunucu Kapatiliyor...");
       
    }
}

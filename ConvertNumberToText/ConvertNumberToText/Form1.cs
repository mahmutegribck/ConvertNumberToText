using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConvertNumberToText
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        TextBox txtSayi = new TextBox();        //Textbox olusturuldu
        Label lblYazi = new Label();            //Label olusturuldu
        private void Form1_Load(object sender, EventArgs e)
        {
            Size = new Size(620, 300);          //Form boyutlari belirlendi
            this.Text = "FONKSİYON HESABI";     //Form ismi belirlendi
            StartPosition = FormStartPosition.CenterScreen;     //Formun, ekranin ortasindan acilmasi saglanir
            BackColor = Color.Aquamarine;
            //Olusturulan textboxin ozellikleri belirlendi
            txtSayi.Height = 40;
            txtSayi.Width = 130;
            txtSayi.Top = 60;
            txtSayi.Left = 40;
            txtSayi.Font = new Font("Microsoft Sans Serif", 10);
            txtSayi.KeyPress += Degerkontrol;       //Textbox a girilecek verinin integer deger ve ./, dan olusmasi saglanir
            this.Controls.Add(txtSayi);     //Forma eklenmesi saglandi

            //Olusturulan labelin ozellikleri belirlendi
            lblYazi.Height = 25;
            lblYazi.Width = 530;
            lblYazi.Top = 150;
            lblYazi.Left = 30;
            lblYazi.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
            lblYazi.BackColor = Color.Khaki;
            lblYazi.BorderStyle = BorderStyle.Fixed3D;
            this.Controls.Add(lblYazi);     //Forma eklenmesi saglandi
            

            //Formu kapatmaya yarayan buton olusturuldu ve ozellikleri verildi
            Button btnClose = new Button
            {
                Height = 50,
                Width = 100,
                Top = 40,
                Left = 430,
                Text = "KAPAT",
                Font = new Font("Microsoft Sans Serif", 15, FontStyle.Bold),
                ForeColor = Color.Red,
                BackColor = Color.Beige,
                TabIndex = 2

            };
            this.Controls.Add(btnClose);
            btnClose.Click += new EventHandler(BtnClose_click);     //Butona event eklendi

            //Sayiyi yaziya cevirecek buton olusturuldu ve ozellikleri verildi
            Button btnHesapla = new Button
            {
                Height = 50,
                Width = 100,
                Top = 40,
                Left = 290,
                Text = "YAZIYA ÇEVİR",
                Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold),
                ForeColor = Color.Red,
                BackColor = Color.Beige,
                TabIndex = 1
            };
            this.Controls.Add(btnHesapla);      //Buton forma eklendi
            btnHesapla.Click += new EventHandler(BtnHesapla_click);     //Butona event eklendi

            Label lblSayigir = new Label
            {
                Height = 30,
                Width = 300,
                Top = 40,
                Left = 40,
                Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold),
                ForeColor = Color.BurlyWood,
                Text = "SAYI GİRİNİZ:"
            };
            this.Controls.Add(lblSayigir);

            Label lblOkunus = new Label
            {
                Height = 30,
                Width = 300,
                Top = 130,
                Left = 35,
                ForeColor = Color.BurlyWood,
                Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold),
                Text = "GİRİLEN SAYININ OKUNUŞU:"
            };
            this.Controls.Add(lblOkunus);
        }
        private void Degerkontrol(object sender, KeyPressEventArgs e)
        {   //Textbox'a sadece integer deger ve ./, girilmesi saglanir
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',' && !char.IsControl(e.KeyChar);
        }

        //Basamak verilerini tutacak degiskenler tanimlandi
        string onbinler, binler, yuzler, onlar, birler, onlarKurus, birlerKurus;
        int basamak5, basamak4, basamak3, basamak2, basamak1, basamakKurus2, basamakKurus1;
        string lira, kurus;
        private void BtnHesapla_click(object sender, EventArgs e)
        {
            string yazi = "";
            lblYazi.Text = String.Empty;  //Bir onceki islemden kalan yazinin silinmesi saglandi
            string sayi = txtSayi.Text.Replace(",", ".").ToString(); //Virgul kullanilmasi halinde nokta ile degistirilir

            int kontrol = sayi.IndexOf(".");  //Girilen sayida noktanin olup olmadigi kontrol edilir
            if (kontrol < 0)
            {
                lira = sayi;        //Sayini tam kismi
                kurus = "";     //Sayini noktadan sonraki kismi
            }
            else
            {
                lira = sayi.Substring(0, sayi.IndexOf('.'));        //Sayini tam kismi
                kurus = sayi.Substring(sayi.IndexOf('.') + 1);       //Sayini noktadan sonraki kismi

                if (kurus.Length != 2)      //Girilen sayinin kurus kisminin iki sayidan olusmasi saglanir
                {
                    MessageBox.Show("KURUŞ İÇİN İKİ BASAMAK GİRİNİZ");
                    return;
                }
            }

            int tamKisim = Convert.ToInt32(lira);       //Girilen sayinin noktan önceki kısmı int degere donusturulur

            if (lira.Length > 5)        //Girilen sayini 5 basamagi gecmemesi saglanir
            {
                MessageBox.Show("GİRİLEN SAYI MAX 5 BASAMAKLI OLABİLİR");

            }
            else        //Girilen sayinin 5 basamak veya daha az olmasi durumunda islemler yapilir
            {
                if (lira != "") // Bos degilse islemler yapilir
                {
                    if (tamKisim == 0 || tamKisim == 00 || tamKisim == 000 || tamKisim == 0000 || tamKisim == 00000)
                    {
                        lblYazi.Text = "SIFIR TL";
                    }
                    else
                    {
                        basamak5 = tamKisim / 10000;    //Aradigimiz basamaktaki rakam tespit edilir 
                        tamKisim = tamKisim % 10000;    // Bir Sonraki basamagin kontrol edilmesi icin mod islemi yapilir
                        switch (basamak5)
                        {
                            case 0: onbinler = ""; break;
                            case 1: onbinler = "ON "; break;
                            case 2: onbinler = "YİRMİ "; break;
                            case 3: onbinler = "OTUZ "; break;
                            case 4: onbinler = "KIRK "; break;
                            case 5: onbinler = "ELLİ "; break;
                            case 6: onbinler = "ALTMIŞ "; break;
                            case 7: onbinler = "YETMİŞ "; break;
                            case 8: onbinler = "SEKSEN "; break;
                            case 9: onbinler = "DOKSAN"; break;
                        }

                        basamak4 = tamKisim / 1000;
                        tamKisim = tamKisim % 1000;
                        //Girilen sayinin 5 basamakli olmasi durumunda dördüncü basamagin sifir olabileceginden dolayı iki durum olusur ve bu durumlar karar bloklariyla belirlenir
                        if (lira.Length == 5)
                        {
                            switch (basamak4)
                            {
                                case 0: binler = "BİN "; break;     //Eger sayi bes basamakli ise dorduncu basamagin sifir olmasi durumunda yaziya bin kelimesi eklenir
                                case 1: binler = "BİR BİN "; break;
                                case 2: binler = "İKİ BİN "; break;
                                case 3: binler = "ÜÇ BİN "; break;
                                case 4: binler = "DÖRT BİN "; break;
                                case 5: binler = "BEŞ BİN "; break;
                                case 6: binler = "ALTI BİN "; break;
                                case 7: binler = "YEDİ BİN "; break;
                                case 8: binler = "SEKİZ BİN "; break;
                                case 9: binler = "DOKUZ BİN "; break;
                            }

                        }
                        else
                        {
                            switch (basamak4)
                            {
                                case 0: binler = ""; break;     //Sayinin dort basamakli veya daha az basamakli olmasi durumunda bin kelimesi eklenmemesi saglanir
                                case 1: binler = "BİR BİN "; break;
                                case 2: binler = "İKİ BİN "; break;
                                case 3: binler = "ÜÇ BİN "; break;
                                case 4: binler = "DÖRT BİN "; break;
                                case 5: binler = "BEŞ BİN "; break;
                                case 6: binler = "ALTI BİN "; break;
                                case 7: binler = "YEDİ BİN "; break;
                                case 8: binler = "SEKİZ BİN "; break;
                                case 9: binler = "DOKUZ BİN "; break;
                            }
                        }

                        basamak3 = tamKisim / 100;
                        tamKisim = tamKisim % 100;
                        switch (basamak3)
                        {
                            case 0: yuzler = ""; break;
                            case 1: yuzler = "YÜZ "; break;
                            case 2: yuzler = "İKİ YÜZ "; break;
                            case 3: yuzler = "ÜÇ YÜZ "; break;
                            case 4: yuzler = "DÖRT YÜZ "; break;
                            case 5: yuzler = "BEŞ YÜZ "; break;
                            case 6: yuzler = "ALTI YÜZ "; break;
                            case 7: yuzler = "YEDİ YÜZ "; break;
                            case 8: yuzler = "SEKİZ YÜZ "; break;
                            case 9: yuzler = "DOKUZ YÜZ "; break;
                        }

                        basamak2 = tamKisim / 10;
                        tamKisim = tamKisim % 10;
                        switch (basamak2)
                        {
                            case 0: onlar = ""; break;
                            case 1: onlar = "ON "; break;
                            case 2: onlar = "YİRMİ "; break;
                            case 3: onlar = "OTUZ "; break;
                            case 4: onlar = "KIRK "; break;
                            case 5: onlar = "ELLİ "; break;
                            case 6: onlar = "ALTMIŞ "; break;
                            case 7: onlar = "YETMİŞ "; break;
                            case 8: onlar = "SEKSEN "; break;
                            case 9: onlar = "DOKSAN "; break;
                        }

                        basamak1 = tamKisim / 1;
                        tamKisim = tamKisim % 1;
                        switch (basamak1)
                        {
                            case 0: birler = ""; break;
                            case 1: birler = "BİR "; break;
                            case 2: birler = "İKİ "; break;
                            case 3: birler = "ÜÇ "; break;
                            case 4: birler = "DÖRT "; break;
                            case 5: birler = "BEŞ "; break;
                            case 6: birler = "ALTI "; break;
                            case 7: birler = "YEDİ "; break;
                            case 8: birler = "SEKİZ "; break;
                            case 9: birler = "DOKUZ "; break;
                        }
                        //Girilen sayini noktadan onceki bolumunun yaziya cevirilmis hali olusturulur 
                        lblYazi.Text = onbinler + binler + yuzler + onlar + birler + "TL ";
                    }
                }
                if (kurus != "")        //Noktadan sonraki bolum varsa islemler yapilir
                {
                    int kurusKisim = Convert.ToInt32(kurus);

                    if (kurusKisim == 00)      //Girilen sayinin virgülden sonraki kismi sifir ise sisfir kurus eklenir 
                    {
                        lblYazi.Text += " SIFIR KURUŞ";
                    }
                    else
                    {
                        basamakKurus2 = kurusKisim / 10;
                        kurusKisim = kurusKisim % 10;
                        switch (basamakKurus2)
                        {
                            case 0: onlarKurus = ""; break;
                            case 1: onlarKurus = "ON "; break;
                            case 2: onlarKurus = "YİRMİ "; break;
                            case 3: onlarKurus = "OTUZ "; break;
                            case 4: onlarKurus = "KIRK "; break;
                            case 5: onlarKurus = "ELLİ "; break;
                            case 6: onlarKurus = "ALTMIŞ "; break;
                            case 7: onlarKurus = "YETMİŞ "; break;
                            case 8: onlarKurus = "SEKSEN "; break;
                            case 9: onlarKurus = "DOKSAN "; break;
                        }
                        basamakKurus1 = kurusKisim / 1;
                        kurusKisim = kurusKisim % 1;
                        switch (basamakKurus1)
                        {
                            case 0: birlerKurus = ""; break;
                            case 1: birlerKurus = "BİR "; break;
                            case 2: birlerKurus = "İKİ "; break;
                            case 3: birlerKurus = "ÜÇ "; break;
                            case 4: birlerKurus = "DÖRT "; break;
                            case 5: birlerKurus = "BEŞ "; break;
                            case 6: birlerKurus = "ALTI "; break;
                            case 7: birlerKurus = "YEDİ "; break;
                            case 8: birlerKurus = "SEKİZ "; break;
                            case 9: birlerKurus = "DOKUZ "; break;
                        }
                        //Girilen sayini noktadan onceki bolumunun devamina noktdan sonraki kisminin yaziya cevirilmis hali eklenir 
                        lblYazi.Text += onlarKurus + birlerKurus + "KURUŞ";
                    }
                }
            }
        }
        private void BtnClose_click(object sender, EventArgs e)
        {
            Close();   //Butona tiklanarak uygulamanin kapanmasi saglanir 
        }
    }
}

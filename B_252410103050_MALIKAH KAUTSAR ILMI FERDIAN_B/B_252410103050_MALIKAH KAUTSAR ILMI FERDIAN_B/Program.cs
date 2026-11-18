using System;

class Agen
{
    public string idAgen;
    public string namaAgen;
    public int stokAgen;

    public void TampilInfo()
    {
        Console.WriteLine("ID: " + idAgen + ", Nama: " + namaAgen + ", Stok: " + stokAgen);
    }
}

abstract class ProdukGalon
{
    private string idProduk;
    private string namaProduk;
    private int stokGudang;

    public string IdProduk
    {
        get { return idProduk; }
        set { idProduk = value; }
    }

    public string NamaProduk
    {
        get { return namaProduk; }
        set { namaProduk = value; }
    }

    public int StokGudang
    {
        get { return stokGudang; }
        set
        {
            if (value < 0)
            {
                Console.WriteLine("Stok gudang tidak boleh negatif!");
            }
            else
            {
                stokGudang = value;
            }
        }
    }

    public void KurangiStok(int jumlah)
    {
        if (jumlah <= 0)
        {
            Console.WriteLine("Jumlah pengurangan harus lebih dari 0!");
        }
        else if (jumlah > stokGudang)
        {
            Console.WriteLine("Stok gudang tidak cukup!");
        }
        else
        {
            stokGudang = stokGudang - jumlah;
        }
    }

    public void TampilInfo()
    {
        Console.WriteLine("ID: " + idProduk + ", Nama: " + namaProduk + ", Stok Gudang: " + stokGudang);
    }

    public abstract int HitungBiaya(int jumlah);
}

class GalonAkua : ProdukGalon
{
    public int harga = 20000;

    public override int HitungBiaya(int jumlah)
    {
        return jumlah * harga;
    }
}

class GalonBerkeran : ProdukGalon
{
    public int harga = 12000;
    public string jenisKeran;

    public override int HitungBiaya(int jumlah)
    {
        return jumlah * harga;
    }
}

interface DapatDidistribusikan
{
    void KirimPaket();
}

class DistribusiGalon : DapatDidistribusikan
{
    public Agen agen;
    public ProdukGalon galon;
    public int jumlah;

    public void KirimPaket()
    {
        if (jumlah <= 0)
        {
            Console.WriteLine("Pengiriman GAGAL, jumlah galon harus lebih dari 0");
        }
        else if (jumlah > galon.StokGudang)
        {
            Console.WriteLine("Pengiriman GAGAL, jumlah melebihi stok gudang... stok:" + galon.StokGudang);
        }
        else
        {
            galon.KurangiStok(jumlah);
            agen.stokAgen = agen.stokAgen + jumlah;
            Console.WriteLine("Paket berhasil di kirimkan ke agen " + agen.namaAgen + " sebanyak " + jumlah + " galon");
        }
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("DATA AGEN");
        Agen agen1 = new Agen();
        agen1.idAgen = "AAA";
        agen1.namaAgen = "Alamalama";
        agen1.stokAgen = 12;
        agen1.TampilInfo();

        Agen agen2 = new Agen();
        agen2.idAgen = "BBB";
        agen2.namaAgen = "Babamarica";
        agen2.stokAgen = 23;
        agen2.TampilInfo();

        GalonAkua akua = new GalonAkua();
        akua.IdProduk = "GAL1";
        akua.NamaProduk = "Galon Akua";
        akua.StokGudang = 50;

        GalonBerkeran berkeran = new GalonBerkeran();
        berkeran.IdProduk = "GAL2";
        berkeran.NamaProduk = "Galon Berkeran";
        berkeran.StokGudang = 30;
        berkeran.jenisKeran = "Keran Putar";

        Console.WriteLine();
        Console.WriteLine("POLYMORPHISM");
        ProdukGalon produk;
        produk = akua;
        Console.WriteLine("Biaya galon akua (2): " + produk.HitungBiaya(2));

        produk = berkeran;
        Console.WriteLine("Biaya galon keran (5): " + produk.HitungBiaya(5));

        Console.WriteLine();
        Console.WriteLine("DISTRIBUSI GALON");

        DistribusiGalon kirim1 = new DistribusiGalon();
        kirim1.agen = agen1;
        kirim1.galon = akua;
        kirim1.jumlah = 10;
        kirim1.KirimPaket();

        DistribusiGalon kirim2 = new DistribusiGalon();
        kirim2.agen = agen2;
        kirim2.galon = berkeran;
        kirim2.jumlah = 0;
        kirim2.KirimPaket();

        DistribusiGalon kirim3 = new DistribusiGalon();
        kirim3.agen = agen2;
        kirim3.galon = berkeran;
        kirim3.jumlah = 100;
        kirim3.KirimPaket();
    }
}
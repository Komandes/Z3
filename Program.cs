using System;

class Program
{
    static void Main()
    {
        try
        {
            Wektor v1 = new Wektor(1, 2, 3);
            Wektor v2 = new Wektor(4, 5, 6);

            Wektor suma = v1 + v2;
            Console.WriteLine("Suma: " + suma);

            Wektor różnica = v2 - v1;
            Console.WriteLine("Różnica: " + różnica);

            Wektor iloczyn = v1 * 2.5;
            Console.WriteLine("Iloczyn: " + iloczyn);

            Wektor iloraz = v2 / 2;
            Console.WriteLine("Iloraz: " + iloraz);

            double? iloczynSkalarny = Wektor.IloczynSkalarny(v1, v2);
            Console.WriteLine("Iloczyn skalarny: " + iloczynSkalarny);
            
            Wektor sumaWektorów = Wektor.Suma(v1, v2, iloczyn, iloraz);
            Console.WriteLine("Suma wektorów: " + sumaWektorów);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Wystąpił wyjątek: " + ex.Message);
        }
    }
}

public class Wektor
{
    private double[] współrzędne;

    public Wektor(byte wymiar)
    {
        współrzędne = new double[wymiar];
    }

    public Wektor(params double[] współrzędne)
    {
        this.współrzędne = współrzędne;
    }

    public double Długość
    {
        get { return Math.Sqrt(IloczynSkalarny(this, this) ?? 0); }
    }

    public byte Wymiar
    {
        get { return (byte)współrzędne.Length; }
    }

    public double this[byte indeks]
    {
        get { return współrzędne[indeks]; }
        set { współrzędne[indeks] = value; }
    }

    public static double? IloczynSkalarny(Wektor V, Wektor W)
    {
        if (V.Wymiar != W.Wymiar)
            return null;

        double iloczyn = 0;
        for (byte i = 0; i < V.Wymiar; i++)
            iloczyn += V[i] * W[i];

        return iloczyn;
    }

    public static Wektor Suma(params Wektor[] Wektory)
    {
        if (Wektory.Length == 0)
            throw new ArgumentException("Brak wektorów do zsumowania.");

        byte wymiar = Wektory[0].Wymiar;
        foreach (Wektor wektor in Wektory)
        {
            if (wektor.Wymiar != wymiar)
                throw new ArgumentException("Wektory muszą mieć ten sam wymiar.");
        }

        Wektor suma = new Wektor(wymiar);
        for (byte i = 0; i < wymiar; i++)
        {
            foreach (Wektor wektor in Wektory)
                suma[i] += wektor[i];
        }

        return suma;
    }

    public static Wektor operator +(Wektor v1, Wektor v2)
    {
        if (v1.Wymiar != v2.Wymiar)
            throw new ArgumentException("Wektory muszą mieć ten sam wymiar.");

        Wektor suma = new Wektor(v1.Wymiar);
        for (byte i = 0; i < v1.Wymiar; i++)
            suma[i] = v1[i] + v2[i];

        return suma;
    }

    public static Wektor operator -(Wektor v1, Wektor v2)
    {
        if (v1.Wymiar != v2.Wymiar)
            throw new ArgumentException("Wektory muszą mieć ten sam wymiar.");

        Wektor różnica = new Wektor(v1.Wymiar);
        for (byte i = 0; i < v1.Wymiar; i++)
            różnica[i] = v1[i] - v2[i];

        return różnica;
    }

    public static Wektor operator *(Wektor v, double skalar)
    {
        Wektor wynik = new Wektor(v.Wymiar);
        for (byte i = 0; i < v.Wymiar; i++)
            wynik[i] = v[i] * skalar;

        return wynik;
    }

    public static Wektor operator *(double skalar, Wektor v)
    {
        return v * skalar;
    }

    public static Wektor operator /(Wektor v, double skalar)
    {
        if (skalar == 0)
            throw new DivideByZeroException("Dzielenie przez zero.");

        Wektor wynik = new Wektor(v.Wymiar);
        for (byte i = 0; i < v.Wymiar; i++)
            wynik[i] = v[i] / skalar;

        return wynik;
    }

    public override string ToString()
    {
        return string.Join(", ", współrzędne);
    }
}
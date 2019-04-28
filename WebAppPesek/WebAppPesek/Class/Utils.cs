using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.Model;

namespace WebAppPesek.Class
{
    public class Utils
    {
        public static double VypoctiPercentRozpoctu(Rozpocet rozpocet)
        {
            double max = rozpocet.Velikost;
            IList<PolozkaRozpoctu> polozky = rozpocet.Polozky;
            double soucet = 0;
            foreach (var polozka in polozky)
            {
                soucet += polozka.Cena;
            }

            return Math.Ceiling((soucet / max) * 100);
        }

        public static bool JeRozpocetAktivni(Rozpocet rozpocet)
        {
            return DateTime.Now > rozpocet.PlatnyOd && DateTime.Now < rozpocet.PlatnyDo;
        }

        public static bool ZvalidujCas(DateTime casOd, DateTime casDo)
        {
            return casOd < casDo;
        }

        public static bool JeNovaVelikostDostacujici(Rozpocet rozpocet, double novaVelikost)
        {
            double soucet = 0;
            if (rozpocet.Polozky != null && rozpocet.Polozky.Count != 0)
            {
                soucet = SpocitejRozpocet(rozpocet);
            }

            return novaVelikost > soucet;
        }

        public static bool JeVRozpoctuVolno(Rozpocet rozpocet, double novaPolozka)
        {
            double soucet;
            if (rozpocet.Polozky == null || rozpocet.Polozky.Count == 0)
            {
                return novaPolozka < rozpocet.Velikost;
            }
            else
            {
                soucet = SpocitejRozpocet(rozpocet);
            }

            return ((soucet + novaPolozka) < rozpocet.Velikost);
        }

        public static double SpocitejRozpocet(Rozpocet rozpocet)
        {
            double soucet = 0;

            foreach (var polozka in rozpocet.Polozky)
            {
                soucet += polozka.Cena;
            }

            return soucet;
        }

        public static int getNowYear()
        {
            return DateTime.Now.Year;
        }

        public static double SpocitejNakladyNaVozidlo(Vozidlo vozidlo)
        {
            double soucet = 0;
            IList<ServisniZaznam> servisniZaznamy = vozidlo.ServisniZaznamy;
            if (servisniZaznamy != null && servisniZaznamy.Count > 0)
            {
                foreach (var polozka in servisniZaznamy)
                {
                    soucet += (double) polozka.Cena;
                }
            }
            else return 0;
            
            return soucet;
        }

        public static bool JeStkBlizkoKExpiraci(DateTime to, int days)
        {
            DateTime from = DateTime.Now;
            
            TimeSpan elapsed = new TimeSpan(to.Ticks - from.Ticks);

            if (elapsed.Days < days)
            {
                return true;
            }

            return false;
        }

        public static string[] zvalidujRozpocty(IList<Rozpocet> rozpocty)
        {
            string[] pole = new string[rozpocty.Count];
            for (int i = 0; i < rozpocty.Count; i++)
            {
                if (!Utils.JeRozpocetAktivni(rozpocty[i]))
                {
                    pole[i] = "alert-danger";
                }
                else
                {
                    pole[i] = "";
                }
            }

            return pole;
        }

        public static string[] zvalidujStk(IList<Vozidlo> vozidla, int daysToExpire)
        {
            string[] pole = new string[vozidla.Count];
            for (int i = 0; i < vozidla.Count; i++)
            {
                if (Utils.JeStkBlizkoKExpiraci(vozidla[i].PlatnostSTK, daysToExpire))
                {
                    pole[i] = "alert-danger";
                }
                else
                {
                    pole[i] = "";
                }
            }

            return pole;
        }
    }
}
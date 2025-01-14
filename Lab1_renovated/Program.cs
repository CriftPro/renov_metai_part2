﻿using CsvHelper;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Formats.Asn1;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Net;
using System.Xml.Linq;

static void Main(string[] args) { 
var path = @"C:\Users\cicui\Documents\\apartment_buildings_2019.csv"; //change path if needed
int max_metai = 0;
int min_metai = 2022;
int yearsort = 2;
int r = 0;
int s = 0;
    int bucket = 0;
string[] renov=null;
Dictionary<int, Namas> namai = new Dictionary<int, Namas>();
using (TextFieldParser csvParser = new TextFieldParser(path))
{
    csvParser.SetDelimiters(new string[] { ";" });
    csvParser.ReadLine();

    while (!csvParser.EndOfData)
    {
        string[] sarasas = csvParser.ReadFields();
        string Id = sarasas[0];
        string adresas = sarasas[1];
        string namo_valdytojas = sarasas[2];
        string valdymo_forma = sarasas[3];
        string paskyrimo_pagrindas = sarasas[4];
        string administratoriaus_pabaigos_Data = sarasas[5];
        string paskirtis = sarasas[6];
        string uni_nr = sarasas[7];
        string bendr_plotas = sarasas[8];
        string naud_plotas = sarasas[9];
        string build_year = sarasas[10];
        string renov_metai = sarasas[11];
        string renovacijos_statusas = sarasas[12];
        string energ_naudingumo_klase = sarasas[13];
        string butu_skaicius = sarasas[14];
        string negyvenamuju_palapu_skaicius = sarasas[15];
        string korpusas = sarasas[16];
        string sklypo_plotas = sarasas[17];
        s = s + 1;
        int.TryParse(Id, out int id);
        int.TryParse(bendr_plotas, out int bendras_plotas);
        int.TryParse(naud_plotas, out int naudotas_plotas);
        int.TryParse(butu_skaicius, out int butu_skaicius_name);
        int.TryParse(negyvenamuju_palapu_skaicius, out int tusciu_butu_skaicius);
        int.TryParse(sklypo_plotas, out int sklypo_bendrasisPlotas);

        DateTime.TryParse(administratoriaus_pabaigos_Data, out DateTime pabaigos_data);
        DateTime.TryParse(build_year, out DateTime pastatymo_metai);
        DateTime.TryParse(renov_metai, out DateTime renovacijos_metai);

        if (renovacijos_statusas == "Renovuotas")
        {

            renov = sarasas;
            AddToDictionary(namai, id, adresas, namo_valdytojas, valdymo_forma, paskyrimo_pagrindas, pabaigos_data, paskirtis, uni_nr, bendras_plotas, naudotas_plotas, pastatymo_metai, renovacijos_metai, renovacijos_statusas, energ_naudingumo_klase, butu_skaicius_name, tusciu_butu_skaicius, korpusas, sklypo_bendrasisPlotas);
            try
            {
                int metai_num = Int32.Parse(renov[11]);
                if (metai_num > max_metai)
                    max_metai = metai_num;
                if (metai_num < min_metai)
                    min_metai = metai_num;
            }
            catch (FormatException)
            {
                Console.WriteLine($"Unable to parse '{renov[11]}'");
            }
            r = r + 1;
        }
    }

            int bucket_num = ((max_metai - min_metai) / yearsort) + 1;
            List<string>[] buckets = new List<string>[bucket_num];
            for (int i = 0; i < bucket_num; i++)
            {
                buckets[i] = new List<string>();
            }
        
            for (int i = 0; i < renov.Length; i++)
            {
                 bucket = ((Int32.Parse(renov[11]) - 2000) / yearsort);
        buckets[bucket].AddRange(renov);
            
        }

        DisplayConsole(buckets, bucket_num);

    }
    
}


static void DisplayConsole(List<string>[] buckets, int bucket_num)
{
    Console.WriteLine("---START WRITING---");
    Console.WriteLine(buckets[1]);
   List<string>[] buck = new List<string>[bucket_num];

    //for (int i = 0; i < bucket_num; i++)
    {
     //  buck[i] = new List<string>();
    }

    //foreach (string[] buck in buckets)
    {
        for (int j = 0; j < bucket_num; j++)
        {
            for (int i = 0; i < buckets.Length; i++)
            {
                Console.WriteLine(buck[j][i]);
            }
        }
    }
    Console.WriteLine("");
    Console.WriteLine("---END WRITING---");


}
static void AddToDictionary(Dictionary<int, Namas> elements, int Id, string address, string owner, string ownershipForm, string appointmentBasis, DateTime adminstrationEndDate, string purpose, string uniNumber, int generalSize, int usedSize, DateTime buildYear, DateTime renovationYear, string renovationStatus, string energyClass, int houseCount, int emtyHouseCount, string corpus, int areaSize)
{
    Namas theElement = new Namas();

    theElement.Id = Id;
    theElement.adresas = address;
    theElement.namo_valdytojas = owner;
    theElement.valdymo_forma = ownershipForm;
    theElement.paskyrimo_pagrindas = appointmentBasis;
    theElement.administratoriaus_pabaigos_Data = adminstrationEndDate;
    theElement.uni_nr = uniNumber;
    theElement.bendr_plotas = generalSize;
    theElement.naud_plotas = usedSize;
    theElement.build_year = buildYear;
    theElement.renov_metai = renovationYear;
    theElement.renovacijos_statusas = renovationStatus;
    theElement.energ_naudingumo_klase = energyClass;
    theElement.butu_skaicius = houseCount;
    theElement.negyvenamuju_palapu_skaicius = emtyHouseCount;
    theElement.korpusas = corpus;
    theElement.sklypo_plotas = areaSize;



    elements.Add(key: theElement.Id, value: theElement);

}
public class Namas
{
    public int Id { get; set; }
    public string adresas { get; set; }
    public string namo_valdytojas { get; set; }
    public string valdymo_forma { get; set; }
    public string paskyrimo_pagrindas { get; set; }
    public DateTime administratoriaus_pabaigos_Data { get; set; }
    public string paskirtis { get; set; }
    public string uni_nr { get; set; }
    public int bendr_plotas { get; set; }
    public int naud_plotas { get; set; }
    public DateTime build_year { get; set; }
    public DateTime renov_metai { get; set; }
    public string renovacijos_statusas { get; set; } 
    public string energ_naudingumo_klase { get; set; }
    public int butu_skaicius { get; set; }
    public int negyvenamuju_palapu_skaicius { get; set; }
    public string korpusas { get; set; }
    public int sklypo_plotas { get; set; }
}



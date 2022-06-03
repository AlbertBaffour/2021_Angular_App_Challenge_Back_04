using dcode_shop_back.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dcode_shop_back.Models
{
    public class DBinitializer
    {
        public static void Initialize(ShopContext context)
        {
            context.Database.EnsureCreated();

            // Look for any products.
            if (context.Products.Any())
            {
                return;   // DB has been seeded
            }

            //Customers
            Customer customer1 = new Customer()
            {
                FirstName = "SAd",
                LastName = "Min",
                Email = "sadmin@dsport.be",
                StreetAndNumber = "Steenweg 2",
                Postcode = "1234",
                City = "Brussel",
                Phone = "+32458975625",

            }; Customer customer2 = new Customer()
            {
                FirstName = "Ad",
                LastName = "Min",
                Email = "admin@dsport.be",
                StreetAndNumber = "Steenweg 10",
                Postcode = "1234",
                City = "Brussel",
                Phone = "+32458975625",

            }; Customer customer3 = new Customer()
            {
                FirstName = "Bennie",
                LastName = "Echt",
                Email = "bennie.echt@gmail.be",
                StreetAndNumber = "Steenweg 1",
                Postcode = "1224",
                City = "Antwerpen",
                Phone = "+32458975625",

            };

            context.Add(customer1);
            context.Add(customer2);
            context.Add(customer3);

            //Add users
            User Sadmin = new User()
            {
                FirstName = "SAd",
                LastName = "Min",
                Email = "sadmin@dsport.be",
                Password = "yQbOT9JStPNZpIaEjAMRyQ==",
                IsActive = true,
                IsAdmin = true,
                IsSuperAdmin = true,
                CustomerId = 1
            };

            User admin = new User()
            {
                FirstName = "Ad",
                LastName = "Min",
                Email = "admin@dsport.be",
                Password = "izPYO12ioMNL4xJe6AxKNg==",
                IsActive = true,
                IsAdmin = true,
                IsSuperAdmin = false,
                CustomerId = 2

            };

            User regular = new User()
            {
                FirstName = "Bennie",
                LastName = "Echt",
                Email = "bennie.echt@gmail.be",
                Password = "nVVyHGO+rCJj0H9HSz27eA==",
                IsActive = true,
                IsAdmin = false,
                IsSuperAdmin = false,
                CustomerId = 3,
            };

            context.Add(Sadmin);
            context.Add(admin);
            context.Add(regular);
            //Add categories

            context.Add(new Category { Name = "Voeding", Description = "categorie voor voeding" });
            context.SaveChanges();          
            context.Add(new Category { Name = "Drank", Description = "categorie voor dranken" });
            context.SaveChanges();
            context.Add(new Category { Name = "Shakes", Description = "categorie voor shakes" });
            context.SaveChanges();
            context.Add(new Category { Name = "Supplementen", Description = "categorie voor supplementen" });
            context.SaveChanges();
            context.Add(new Category { Name = "Merchandise", Description = "categorie voor merchandise" });
            context.SaveChanges();
            //Add products
            context.AddRange(
                //voeding
                new Product { Name = "Foodspring Energy Gel (12 Gels)", Description = "Heeft u een gel nodig voor snelle energie? De Foodspring Energy Gel is uw beste bondgenoot tijdens intensieve inspanningen en bij het naderen van de finish.", Brand = "Merkloos", Img = "sv1_sgfoqs.webp", Price = 24.99M, CategoryId = 1, QuantityInStock = 10, IsActive = true },
                new Product { Name = "Powerbar Cola (met Cafeïne) - 1,44kg (24 Gels)", Brand = "Merkloos", Description = "Maximale energie. Heel licht verteerbaar dankzij het lage vetgehalte. Bevat een natriumsupplement om het verlies door transpiratie te compenseren.", Img = "sv2_vx1ona.webp", Price = 39.90M, CategoryId = 1, QuantityInStock = 10, IsActive = true },
                new Product { Name = "Foodspring Energy Gel (12 Gels)",Description= "Heeft u een gel nodig voor snelle energie? De Foodspring Energy Gel is uw beste bondgenoot tijdens intensieve inspanningen en bij het naderen van de finish.", Brand = "Merkloos", Img = "sv3_ssm6zw.webp", Price = 24.99M, CategoryId = 1, QuantityInStock = 10, IsActive = true },
                new Product { Name = "Isostar Actifood Exotique",Brand="Isostar", Description = "Isostar Fruit Gel Energy Actifood Exotic is een energiegel met exotische smaak, met echte stukjes fruit. Te gebruiken binnen het kader van een gezond en evenwichtig voedingspatroon en leefwijze, beantwoordt dit product aan de noden en behoeften van de spieren tijdens een fysieke inspanning.", Img = "sv4_t9psyh.webp", Price = 2.79M, CategoryId = 1, QuantityInStock = 6, IsActive = true },
                new Product { Name = "Prozis Energy Gel - 25g (24 Gels)", Description = "Dankzij de gemakkelijke verteerbare koolhydraten en essentiële aminozuren van Prozis Energy Gel krijgt je prestatie meer kracht wanneer je dat extra duwtje nodig hebt. Koolhydraten zijn nu eenmaal je belangrijkste energiebron tijdens intensief en/of langdurig sporten, zoals hardlopen, mountainbiken of voetballen.", Img = "sv5_hag9j2.webp", Brand="Prozis", Price = 16.56M, CategoryId = 1, QuantityInStock = 5, IsActive = true },
                new Product { Name = "Energy Gels 'Endurance' - 40g (20 Gels)", Description = "Deze 100% natuurlijke Energy Gel geeft je snel en langdurig een gezonde boost. Hij is gemaakt op basis van een krachtig kruidencomplex met antioxidanten.", Img = "sv6_c6xqfn.webp", Price = 59.45M, CategoryId = 1, QuantityInStock = 4, IsActive = true },
                new Product { Name = "SiS Go Isotonic Sachet - 60ml",Description= "Deze SiS Go Isotonic Energygel is een ideale sportgel voor een snelle energieboost. De gel heeft een lekkere lemon & lime smaak", Brand = "Merkloos", Img = "sv7_en2dgi.webp", Price = 1.95M, CategoryId = 1, QuantityInStock = 5, IsActive = true },
                new Product { Name = "Powergel Shots Cola - 60g (24 Gels)", Description = "The PowerBar Shots zijn een alternatief voor klassieke koolhydraatrepen en gels. Net als de PowerGel's bevatten ze C2MAX, een wetenschappelijk ontwikkelde combinatie van koolhydraten met een 2:1 verhouding van glucose- en fructosebronnen.", Brand = "Merkloos", Img = "sv8_hwlcai.webp", Price = 35.99M, CategoryId = 1, QuantityInStock = 10, IsActive = true },
                new Product { Name = "All Day 100% natuurlijke noten", Description = "Noten met veel natuurlijke proteine voor krachttraining.", Brand = "Merkloos", Img = "sv9_lh1zbt.webp", Price = 13.45M, CategoryId = 1, QuantityInStock = 10, IsActive = true },
                new Product { Name = "Pro Bar Cookie Dough F. (12 Repen)", Description = "Pro Bar is de eiwitreep met het meeste eiwit tegenover de kleinste hoeveelheid calorieën. Onze Pro Bar bevat maar liefst 25g eiwit, 0.8g suikers en slecht 180 kcal! Pro Bar is de eiwitreep met waanzinnig goede voedingswaardes, voor fanatieke sporters en in je low carb- en eiwitdieet.", Brand = "Body & fit", Img = "sv10_pgqa2b.webp", Price = 14.99M, CategoryId = 1, QuantityInStock = 0, IsActive = true },
                new Product { Name = "Vitamin & Protein Bar Mix Box - 60g (10 Repen)", Description = "Wil je een eiwitrijke snackreep met verschillende voordelen? Zoals de naam al aangeeft, zit de Vitamin & Protein Bar van Fulfil bomvol met negen vitaminen en 60 g hoogwaardige eiwitten die helpen bij het opbouwen en behouden van spiermassa.", Brand = "Merkloos", Img = "sv11_c1jhch.webp", Price = 22.90M, CategoryId = 1, QuantityInStock = 2, IsActive = true },
                new Product { Name = "Smart Bar Crunchy",Brand="Body & fit" , Description = "Smart Bars Crunchy bevat per eiwitreep 15 gram hoogwaardig eiwit, 0 toegevoegde suikers* en slechts 164 kcal! Eiwitten ondersteunen sterke spieren, instandhouding van spieren en herstel van spieren bij training.", Img = "sv12_octley.webp", Price = 15.99M, CategoryId = 1, QuantityInStock = 4, IsActive = true },
                new Product { Name = "Prozis Protein Snack - 30g (12 Repen)", Description = "De hele dag zitten tijdens het werk? Proef het eenvoudig en geniet van je eiwitdosis zonder extra calorieën. Het is fitlicious!", Brand = "Merkloos", Img = "sv13_iku3u8.webp", Price = 9.48M, CategoryId = 1, QuantityInStock = 5, IsActive = false },

                //Drank
                new Product { Name = "Energy Drink Caffeine - 2.2kg", Description = "Energy Drink Cafeïne is een wetenschappelijk samengestelde oplossing van koolhydraten, cafeïne en elektrolyten om u te helpen uw prestaties te behouden en uw hydratatie te verbeteren tijdens duurtraining.", Brand = "Merkloos" , Img = "Energy-Drink-Caffeine_Citrus_2200g_Front_RGB_1200x1200-1_zqqjcf.webp", Price = 29.01M, CategoryId = 2, QuantityInStock = 4, IsActive = true },
                new Product { Name = "Slow Release EnergyDrink - 1kg", Description = "Isomaltulose is samengesteld uit glucose en fructose zoals sucrose maar met een andere chemische structuur. Het stimuleert je lichaam om meer vetten te gaan gebruiken. Het is bijzonder gunstig tijdens langdurige trainingperiodes (verbeterde vetstofwisseling, verhoogde vetverbranding).", Brand = "Merkloos",  Img = "HIGH5_Slow-Release-Energy-Drink_1000g_LEMON_wem8vj.webp", Price = 30.00M, CategoryId = 2, QuantityInStock = 4, IsActive = true },
                new Product { Name = "SiS Rego Rapid Recovery", Description = "Een complete, herstellende drank die u helpt om na intensieve sportprestaties te herstellen. Drink meteen na het sporten zodat u snel herstelt.", Brand ="Sis", Img= "sis_rapid_tm58cs.webp", Price = 14.50M, CategoryId = 2, QuantityInStock = 4, IsActive = true },
                new Product { Name = "Low Calorie Meal Ready to Drink", Description = "De perfecte maaltijdvervangende drank om te helpen bij gewichtsbeheersing of gewichtsverlies. Vervang eenvoudig uw maaltijd - het kan ontbijt, lunch of diner zijn - een of twee keer per dag met onze caloriearme maaltijddrank.", Brand = "Merkloos", Img = "lowcal_udwtjy.webp", Price = 12.99M, CategoryId = 2, QuantityInStock = 4, IsActive = true },
                new Product { Name = "Smart Protein Drinks", Description = "Zin in een kant-en-klaar drankje na het sporten in plaats van voor de verandering je shake te mixen? Smart Protein Drink is onze heerlijk smakende eiwitrijke drank, vergelijkbaar met een eiwitshake, maar dan in een handige fles van 250 ml.", Brand = "Merkloos", Img = "smart-protein-drinks_Image_01_vfhwcf.webp", Price = 3.90M, CategoryId = 2, QuantityInStock = 4, IsActive = true },
                new Product { Name = "Nocco BCAA Drink", Description = "Geef jezelf een boost en vul je aminozuren voor, tijdens of na het sporten aan met NOCCO BCAA van de No Carbs Company – een heerlijk smakende, calorievrije, suikervrije drank.", Brand = "Merkloos", Img = "nocco_okcdqh.webp", Price = 2.50M, CategoryId = 2, QuantityInStock = 4, IsActive = true },
                new Product { Name = "AminoPro Drink", Description = "Geef jezelf een energieboost en vul je aminozuren aan met AminoPro van FCB Sweden - een heerlijk smakende, calorievrije, suikervrije drank waarvan je kunt genieten voor, tijdens of na het sporten.", Brand = "",Img = "aminopro-drink_Image_01_rvk2gd.webp", Price = 1.40M, CategoryId = 2, QuantityInStock = 4, IsActive = true },
                new Product { Name = "High Protein Iced Coffee", Description = "Voor alle koffie liefhebbers, doorzetters, dromers met deadlines en ontdekkingsreizigers: deze is voor jullie!", Brand = "High", Img = "highprocoffee_co16so.webp", Price = 1.89M, CategoryId = 2, QuantityInStock = 4, IsActive = true },
                new Product { Name = "Monster Energy Ultra", Description = "Slapen doen we later, vanavond gaan we feesten alsof er geen mañana is! Ultra Fiesta viert de nachten die overgaan in ochtenden en de vrienden die we familie noemen.", Brand ="Monster", Img= "monsterdrink_quk5pz.webp", Price = 2.89M, CategoryId = 2, QuantityInStock = 4, IsActive = true },
                new Product { Name = "Vital Drink Zerop", Description = "Wilt u een caloriearm, suikerarm verfrissend drankje ter ondersteuning van uw dagelijkse levensstijl? Low Carb Vital van Best Body Nutrition bevat vitamine B1, B2 en B6, die samen je energieniveau helpen en vermoeidheid en moeheid verminderen.", Brand = "Merkloos", Img = "low-carb-vital-drink_Image_01_hlvzcm.webp", Price = 9.90M, CategoryId = 2, QuantityInStock = 4, IsActive = false },

                //shakes
                new Product { Name = "First Class Nutrition", Description = "Wat heb je aan een goedkoop eiwit als je lichaam het niet goed opneemt? First Class Nutrition is het enige merk dat een 100% wei-eiwit versterkt met alle drie de belangrijkste probiotica die een veel betere spijsvertering en opname in je lichaam garanderen en is suiker- en aspartaamvrij!", Brand = "House of Nutrition", Img = "fcns_w6p0pn.webp", Price = 16.75M, CategoryId = 3, QuantityInStock = 5, IsActive = true },
                new Product { Name = "Performance Sports Nutrition", Description = "De Turbo Mass Gainer van Performance bevat ingrediënten die jou gaan helpen om je spiermassa te stimuleren en je gewicht doen toenemen. Een ideale combinatie om jouw doelen makkelijker te kunnen halen.", Brand = "Merkloos", Img = "psnpws_ovmuj8.webp", Price = 27.50M, CategoryId = 3, QuantityInStock = 5, IsActive = true },
                new Product { Name = "Whey Isolate 90 Aardbei - 2kg", Description = "Whey Isolate 90 bevat 90% eiwit per portie van 30 g, dus ongeveer 27 g. Dit is de ultieme voedingshulp voor wie zuivere spiermassa wilt opbouwen en behouden.", Brand = "Merkloos", Img = "wi90as_zqhjxe.webp", Price = 59.99M, CategoryId = 3, QuantityInStock = 5, IsActive = true },
                new Product { Name = "XXL Nutrition Weight Gaine 2.5kg", Description = "Weight Gainer is het ideale supplement voor mensen die moeite hebben met het verkrijgen van spiermassa en gewicht. Weight Gainer is een superieure mix van verschillende koolhydraat- en eiwitbronnen en heeft een laag vetgehalte.", Brand = "Merkloos", Img = "xxlnwgs_aaemgj.webp", Price = 29.95M, CategoryId = 3, QuantityInStock = 5, IsActive = true },
                new Product { Name = "Yfood Classic Powder", Description = "Schud jezelf vol! Classic Powder voorziet je lichaam van essentiële voedingsstoffen zoals eiwitten, vezels, plantaardige oliën en 26 vitamines en mineralen.", Brand = "Merkloos", Img = "yfoodcs_dqoabt.webp", Price = 29.99M, CategoryId = 3, QuantityInStock = 5, IsActive = true },
                new Product { Name = "Extreme Pack Plus Bodylab24", Description = "Onze populaire Bodylab Whey Protein is gemaakt van verse wei en biedt een extra hoogwaardige eiwitbron met een snelle absorptietijd (opname).", Brand = "Merkloos", Img = "xtrempackpbs_nt5xza.webp", Price = 75.00M, CategoryId = 3, QuantityInStock = 5, IsActive = true },
                new Product { Name = "M Double You Whey Isolate - 900g", Description = "Deze 100% Whey Protein van M Double You is een koolhydraatarm eiwitpoeder dat bestaat uit 3 verschillende soorten snel opneembare whey proteïnen, namelijk whey protein concentraat, whey protein isolaat en whey protein hydrolysaat.", Brand = "Merkloos", Img = "mdoubleus_einulv.webp", Price = 26.31M, CategoryId = 3, QuantityInStock = 5, IsActive = true },
                new Product { Name = "Impact Whey Protein - 2.5kg", Description = "Hoge kwaliteit whey met 21 g eiwitten per portie, voor de eiwitten die je nodig hebt uit een hoogwaardige bron — dezelfde koeien die je melk en kaas produceren. Het wordt simpelweg gefilterd en gesproeidroogd om volledig natuurlijke voedingsstoffen te produceren.", Brand = "Merkloos", Img = "impwheyproteins_beqzkn.webp", Price = 57.99M, CategoryId = 3, QuantityInStock = 5, IsActive = true },
                new Product { Name = "XXL Nutrition Whey isolaat Vanille - 1kg", Description = "Whey Isolate bevat de allerbeste kwaliteit gefilterde instant whey isolaat. Betere kwaliteit is er simpelweg niet! Whey Isolaat bevat een zo hoog mogelijk eiwitgehalte en nauwelijks lactose of vet.", Brand = "Merkloos", Img = "xxlnwisovan_tdt9ol.webp", Price = 29.95M, CategoryId = 3, QuantityInStock = 5, IsActive = true },

                //Sup
                new Product { Name = "ISO PLUS", Brand = "iso", Description = "Powder is an isotonic and carbohydrate preparation, enriched with L-carnitine and L-glutamine, specially designed for endurance sports.", Img = "isoplus_omnhpf.webp", Price = 9.90M, CategoryId = 4, QuantityInStock = 5, IsActive = true },
                new Product { Name = "B&F 24hr Beauty Essentials (60 x)", Description = "Er zijn veel gezichts-, huid-, nagel- en haarverzorgingsproducten, cosmetica en crèmes verkrijgbaar. Waarschijnlijk gebruik je er één of meerdere en betaal je er soms veel geld voor, maar het effect daarvan is regelmatig te verwaarlozen.", Brand ="B&F",Img= "baf24hrs_v9ksns.webp", Price = 20.99M, CategoryId = 4, QuantityInStock = 5, IsActive = true },
                new Product { Name = "Collageen Peptides", Description = "SR gehydrolyseerde collageenpeptiden leveren 11 gram peptiden met een laag molecuulgewicht. SR® Collagen Peptides zijn geweldig in koffie en eiwitshakes en zijn een gemakkelijke manier om collageen aan te vullen in uw dagelijkse routine.", Brand = "Vital Proteins", Img= "collagpep_zzjeyp.webp", Price = 25.00M, CategoryId = 4, QuantityInStock = 5, IsActive = true },
                new Product { Name = "Selenium 100mcg", Description = "Selenium is een essentieel sporenmineraal dat van nature voorkomt in paranoten, orgaanvlees, zeevruchten en tarwekiemen. 1 tot 2 maal daags 1 tablet bij de maaltijd innemen.", Brand = "Swanson Health", Img= "selenium-100mcg_Image_01_flcmr1.webp", Price = 9.90M, CategoryId = 4, QuantityInStock = 5, IsActive = true },
                new Product { Name = "Ultra Vitamins A, C, E & Selenium", Description = "Als voedingssupplement één softgel per dag met water innemen.", Brand = "Swanson Health", Img= "swansonvit_piimql.webp", Price = 13.90M, CategoryId = 4, QuantityInStock = 5, IsActive = true },
                new Product { Name = "B&F Men's Essentials (60x)", Description = "Als man wil je er goed, fris en verzorgd uitzien. Een frisse uitstraling. Van je 18e tot je 80e! Er zijn veel gezichts-, huid-, nagel- en haarverzorgingsproducten en crèmes verkrijgbaar. Waarschijnlijk gebruik je er één of meerdere en betaal je er soms veel geld voor, maar het effect daarvan is regelmatig te verwaarlozen.", Brand ="B&F",Img= "bafmens_ogfiud.webp", Price = 20.99M, CategoryId = 4, QuantityInStock = 5, IsActive = true },

                //Merchan
               new Product { Name = "Universele Sportarmband Hoesje", Description = "Dit is een zwarte universele sportarmband voor grote telefoons met een grootte tot en met 5.5 inch. Dankzij deze sportarmband kun je je telefoon eenvoudig meenemen tijdens het sporten.", Brand = "Merkloos", Color ="black", Img= "unilooparm_uwqnbd.webp", Price = 6.95M, CategoryId = 5, QuantityInStock = 3, IsActive = true },
               new Product { Name = "Nike Sportarmband Hoesje", Description = "Nike Lean Arm Band Plus phone houder zwart, u houdt uw handen vrij tijdens uw hardloopronde of in de sportschool. Het touchscreen van uw telefoon is gewoon door het beschermende venster heen te gebruiken en strategische openingen voor de knoppen van uw telefoon, poorten en camera maken deze band extra functioneel.", Brand ="Nike",Color="black", Img= "nikelooparm_wanht2.webp", Price = 24.95M, CategoryId = 5, QuantityInStock = 3, IsActive = true },
               new Product { Name = "Adidas - PERF BOTTLE", Description = "Deze waterfles is vaatwasmachinebestendig en BPA-vrij en houdt je goed gehydrateerd terwijl je traint. De handige inkeping maakt het gemakkelijk om je dorst halverwege te lessen.", Brand ="Adidas",Color="blue", Img= "adidaswf_o1x3ra.webp", Price = 5.36M, CategoryId = 5, QuantityInStock = 3, IsActive = true },
               new Product { Name = "PUMA Training Sportstyle bidon", Description = "Deze ultramoderne sportbidon heeft een tijdloos model met een transparante fles en stoere verticale PUMA-logo’s en is perfect voor elke sporter die onderweg is. De schroefdop morst niet en een metalen veiligheidshaak voorkomt morsen en knoeien als je onderweg bent.", Brand ="Puma",Color="black", Img= "pumawaterf_e4iqps.webp", Price = 11.00M, CategoryId = 5, QuantityInStock = 3, IsActive = true },
               new Product { Name = "Elastieken weerstandsbanden set", Description = "Ideaal voor het squatten, trainen van de buikspieren, beenspieren, maar ook voor cardio, yoga, crossfit, krachttraining, pilates en fysiotherapie. Gebruik de weerstandsbanden thuis, op vakantie of in de sportschool!", Brand = "Merkloos", Color ="", Img= "elaspac_hcubce.webp", Price = 17.95M, CategoryId = 5, QuantityInStock = 3, IsActive = true },
               new Product { Name = "Fitness Fit Pakket - Inclusief trainingsschema", Description = "Op zoek naar een cadeau waar je langer en meer plezier van hebt? Ga dan voor een cadeaupakket. Een cadeaupakket is leuk om te geven én om te krijgen. Neem bijvoorbeeld het VirtuFit Fitness Fit Pakket.", Brand = " VirtuFit", Color="", Img= "fittrainpac_yrtjcz.webp", Price = 19.90M, CategoryId = 5, QuantityInStock = 3, IsActive = true }
            
                );


            //Orders
            Order order = new Order()
            {
                OrderDate = DateTime.Now,
                CustomerId = 3,
                Status = Order.status.ordered
            };

            Order order2 = new Order()
            {
                OrderDate = DateTime.Now.AddDays(-7),
                CustomerId = 3,
                Status = Order.status.ordered,
            };
            Order basket1 = new Order()
            {
                CustomerId = 3,
                Status = Order.status.basket,
            };

            context.Add(order);
            context.Add(order2);
            context.Add(basket1);

            //ProductOrders
            OrderProduct op = new OrderProduct()
            {
                OrderId = 1,
                ProductId = 1,
                CurrentPrice = 5.99M,
                Quantity = 5
            };

            OrderProduct op2 = new OrderProduct()
            {
                OrderId = 2,
                ProductId = 2,
                CurrentPrice= 9.99M,
                Quantity = 2
            };

            OrderProduct op3 = new OrderProduct()
            {
                OrderId = 1,
                ProductId = 2,
                CurrentPrice = 9.99M,
                Quantity = 10
            }; 
            OrderProduct op4 = new OrderProduct()
            {
                OrderId = 2,
                ProductId = 4,
                CurrentPrice = 19.99M,
                Quantity = 1
            };
            OrderProduct op5 = new OrderProduct()
            {
                OrderId = 3,
                ProductId = 6,
                CurrentPrice = 19.99M,
                Quantity = 1
            };

            context.Add(op);
            context.Add(op2);
            context.Add(op3);
            context.Add(op4);
            context.Add(op5);
            //favourieten
            Favourite fv1 = new Favourite()
            {
                ProductId = 6,
                CustomerId = 3
            };
            Favourite fv2 = new Favourite()
            {
                ProductId = 5,
                CustomerId = 3
            };

            context.Add(fv1);
            context.Add(fv2);
          

            context.SaveChanges();
        }
    }
}

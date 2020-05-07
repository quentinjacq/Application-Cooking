using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet
{
    class Program
    {
        static int NbClients(MySqlConnection maConnexion, bool CdR)
        {
            maConnexion.Open();
            string where = "";
            if (CdR == true)
            {
                where = "WHERE nbr_recettes_crees > 0";
            }
            string requete = "SELECT count(*) FROM client "+where+";";
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            int nb_client = int.Parse(reader.GetValue(0).ToString());
            reader.Close();
            maConnexion.Close();
            return nb_client;
        }

        static int NbRecette(MySqlConnection maConnexion)
        {
            maConnexion.Open();
            string requete = "SELECT count(*) FROM recette;";
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            int nb_client = int.Parse(reader.GetValue(0).ToString());
            reader.Close();
            maConnexion.Close();
            return nb_client;
        }

        static List<string[]> CdR_recettes(MySqlConnection maConnexion)
        {
            
            maConnexion.Open();
            string requete = "SELECT cli.nom_client, cli.prenom_client, COUNT(*) somme FROM client cli, recette rec WHERE cli.numtel_client = rec.numtel_client AND rec.nbr_commande > 0 AND cli.nbr_recettes_crees>0 GROUP BY cli.nom_client, cli.prenom_client ORDER BY somme DESC;";
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;
            MySqlDataReader reader = command.ExecuteReader();
            List<string[]> top = new List<string[]>();
            while (reader.Read())
            {
                top.Add(new string[] { reader.GetValue(0).ToString(), reader.GetValue(1).ToString(), reader.GetValue(2).ToString() });
            }

            reader.Close();
            maConnexion.Close();
            return top;
        }

        static List<string[]> Liste_Produit(MySqlConnection maConnexion, bool stockmin, bool demo)
        {

            maConnexion.Open();
            string where = "";
            if (stockmin==true)
            {
                if (demo == true)
                {
                    where = "WHERE stock_actuel < (2*stock_min)";
                }
                else
                {
                    where = "WHERE stock_actuel < stock_min";
                }
            }
            string requete = "SELECT nom_produit, stock_min, stock_actuel FROM produit "+where+";";
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;
            MySqlDataReader reader = command.ExecuteReader();
            List<string[]> produit_qte = new List<string[]>();
            while (reader.Read())
            {
                produit_qte.Add(new string[] { reader.GetValue(0).ToString(), reader.GetValue(1).ToString(), reader.GetValue(2).ToString() });
            }

            reader.Close();
            maConnexion.Close();
            return produit_qte;
        }

        static List<string[]> DetailProduit(MySqlConnection maConnexion, string nom_produit)
        {

            maConnexion.Open();
            string requete = "SELECT ut.nom_recette, ut.nombre_necessaire, prod.unite_quant_prod FROM utilise ut, produit prod WHERE ut.nom_produit = prod.nom_produit AND ut.nom_produit = '" + nom_produit + "';";
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;
            MySqlDataReader reader = command.ExecuteReader();
            List<string[]> produit_qte = new List<string[]>();
            while (reader.Read())
            {
                produit_qte.Add(new string[] { reader.GetValue(0).ToString(), reader.GetValue(1).ToString(), reader.GetValue(2).ToString() });
            }

            reader.Close();
            maConnexion.Close();
            return produit_qte;
        }

        static void NvClient(MySqlConnection maConnexion, string numtel_client, string nom_client, string prenom_client, string mail_client, int age, string adresse_client, string ville, string mdp, int nombre_cook, int nbr_recettes_crees, bool Est_gestionnaire_cooking)
        {
            maConnexion.Open();
            string requete = "INSERT INTO `DM`.`client` (`numtel_client`, `nom_client`, `prenom_client`, `mail_client`, `age`, `adresse_client`, `ville`, `mdp`, `nombre_cook`, `nbr_recettes_crees`, `Est_gestionnaire_cooking`) VALUES ('"+ numtel_client +"','"+ nom_client+"','"+ prenom_client+"','"+ mail_client+"',"+ age+",'"+ adresse_client+"','"+ ville+"','"+ mdp+"',"+ nombre_cook+","+ nbr_recettes_crees+","+ Est_gestionnaire_cooking+");";
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;
            MySqlDataReader reader = command.ExecuteReader();
            reader.Close();
            maConnexion.Close();
        }

        static void NvCommande(MySqlConnection maConnexion,DateTime date_commande, int prix_commande,string num_tel_client, string[,] compose_recettes)
        {
            string dateC = date_commande.ToString("yyyy-MM-dd HH:mm:ss");
            maConnexion.Open();

            string requete = "Select MAX(num_commande) from commande;";
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            int num_commande = 1+ int.Parse(reader.GetValue(0).ToString());
            reader.Close();

            requete = "INSERT INTO `DM`.`commande` (`num_commande`, `date_commande`, `prix_commande`, `num_tel_client`) VALUES ("+ num_commande + ", '"+ dateC+"',"+ prix_commande+", '"+ num_tel_client + "');";
            command.CommandText = requete;
            reader = command.ExecuteReader();
            reader.Close();
            for (int i = 0; i < compose_recettes.GetLength(0); i++)
            {
                requete = "INSERT INTO `DM`.`compose` (`num_commande`, `nom_recette`, `nombre_recette_dans_commande`) VALUES("+ num_commande + ", '"+ compose_recettes[i,0] + "', "+ Convert.ToInt32(compose_recettes[i, 1]) + ");";
                command.CommandText = requete;
                reader = command.ExecuteReader();
                reader.Close();
            }
            maConnexion.Close();
        }

        static void NvRecette(MySqlConnection maConnexion,string nom_recette,string type_recette, string descriptif_recette, int prix_recette, int remuneration_cdr,DateTime date_creation, string numtel_client, string[,] utilise_produit, int nbr_commande)
        {
            string dateC = date_creation.ToString("yyyy-MM-dd");
            maConnexion.Open();
            string requete = "INSERT INTO `DM`.`recette` (`nom_recette`, `type_recette`, `descriptif_recette`, `prix_recette`, `remuneration_cdr`, `date_creation`, `numtel_client`, `nbr_commande`) VALUES ('" + nom_recette + "','"+ type_recette + "','"+ descriptif_recette + "', "+ prix_recette + ", "+ remuneration_cdr + ", '"+dateC+"', '"+ numtel_client + "', "+ nbr_commande + ");";
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;
            MySqlDataReader reader = command.ExecuteReader();
            reader.Close();
            maConnexion.Close();

            
            for (int i = 0; i < utilise_produit.GetLength(0); i++)
            {
            //    maConnexion.Open();
            //    reader = command.ExecuteReader();
            //    bool produit_existe = false;
            //    while (reader.Read())
            //    {
            //        for (int j = 0; j < reader.FieldCount; j++)
            //        {
            //            if (reader.GetValue(j).ToString() == utilise_produit[i,0])
            //            {
            //                produit_existe = true;
            //            }
            //        }
            //    }
            //    reader.Close();
            //    maConnexion.Close();
            //
            //    if (produit_existe == false)
            //    {
            //        CreationProduit(maConnexion, utilise_produit[i, 0]);
            //    }

                maConnexion.Open();
                string requete2 = "INSERT INTO `DM`.`utilise` (`nom_recette`, `nom_produit`, `nombre_necessaire`) VALUES ('"+ nom_recette + "','"+ utilise_produit[i, 0] + "', "+ Convert.ToInt32(utilise_produit[i, 1]) + ");";
                MySqlCommand command2 = maConnexion.CreateCommand();
                command2.CommandText = requete2;
                MySqlDataReader reader2 = command2.ExecuteReader();
                reader2.Close();
                maConnexion.Close();
            }
        }

        /**
        static void CreationProduit(MySqlConnection maConnexion, string nom_produit)
        {
            //Blabla le produit n'existe pas, veuillez le remplir
            if (nom_produit == "Anchois")
            {
                NvProduit(maConnexion, nom_produit, "Poisson", "unité", 0, 0, 0);
            }
            else if (nom_produit == "Olive")
            {
                NvProduit(maConnexion, nom_produit, "Fruit", "unité", 0, 0, 0);
            }
        }
    **/
        static void NvProduit(MySqlConnection maConnexion, string nom_produit, string categotie_produit, string unite_quant_prod, int stock_actuel, int stock_min, int stock_max)
        {
            maConnexion.Open();
            string requete = "INSERT INTO `DM`.`produit` (`nom_produit`, `categotie_produit`, `unite_quant_prod`, `stock_actuel`, `stock_min`, `stock_max`) VALUES ('"+nom_produit+"','"+ categotie_produit + "','"+ unite_quant_prod + "', "+stock_actuel+", "+stock_min+", "+ stock_max + ");";
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;
            MySqlDataReader reader = command.ExecuteReader();
            reader.Close();
            maConnexion.Close();
        }

        static int Identification(MySqlConnection maConnexion, string indentificateur, string mdp)
        {
            // ATTTENTIOOOOOOOOOOOOOOOOONNNNNNN IL FAUT QUE CA SOIT IMPOSSIBLE DE RENTRER QUELQUE CHOSE DE NULL
            int id_reussie = -1;
            maConnexion.Open();
            string requete = "Select numtel_client, mdp, Est_gestionnaire_cooking, nbr_recettes_crees, mail_client from client;";
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read() && id_reussie < 0)
            {
                if ((indentificateur == reader.GetValue(0).ToString() || indentificateur == reader.GetValue(4).ToString()) && mdp == reader.GetValue(1).ToString())
                {
                    if (bool.Parse(reader.GetValue(2).ToString()) == true)
                    {
                        id_reussie = 3;
                    }
                    else if (int.Parse(reader.GetValue(3).ToString()) > 0)
                    {
                        id_reussie = 2;
                    }
                    else
                    {
                        id_reussie = 1;
                    }
                }
            }
            reader.Close();
            maConnexion.Close();
            return id_reussie;// -1 si pas réussi, 1 si simple client, 2 si Cooker, 3 si gestionnaire
        }

        static void MiseAJourStockProduit(MySqlConnection maConnexion, string nom_produit, int nombre)
        {
            maConnexion.Open();
            string requete = "SELECT stock_actuel FROM produit WHERE nom_produit = '" + nom_produit + "';";
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            int nouveaustock = int.Parse(reader.GetValue(0).ToString()) + nombre;
            reader.Close();

            requete = "UPDATE produit SET stock_actuel =" + nouveaustock + " WHERE nom_produit = '" + nom_produit + "';";
            command.CommandText = requete;
            reader = command.ExecuteReader();
            reader.Close();
            maConnexion.Close();
        }

        static void EnleverProduitDepuisRecette(MySqlConnection maConnexion, string nom_recette, int nombre)
        {
            maConnexion.Open();
            string requete = "SELECT nom_produit, nombre_necessaire FROM utilise WHERE nom_recette = '" + nom_recette + "';";
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;
            MySqlDataReader reader = command.ExecuteReader();
            List<string[]> produitsnecessaires = new List<string[]>();
            while (reader.Read())
            {
                produitsnecessaires.Add(new string[] { reader.GetValue(0).ToString(), reader.GetValue(1).ToString() });
            }
            reader.Close();

            maConnexion.Close();
            for (int i = 0; i < produitsnecessaires.Count; i++)
            {
                MiseAJourStockProduit(maConnexion, produitsnecessaires[i][0], 0 - (nombre * Convert.ToInt32(produitsnecessaires[i][1])));
            }
        }

        static void MiseAJourCompteurRecette(MySqlConnection maConnexion, string nom_recette, int nombre)
        {
            maConnexion.Open();

            string requete = "SELECT nbr_commande FROM recette WHERE nom_recette = '" + nom_recette + "';";
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            int nbr_commande = int.Parse(reader.GetValue(0).ToString());
            int nv_nbr_commande = nbr_commande + nombre;
            reader.Close();

            requete = "UPDATE recette SET nbr_commande =" + nv_nbr_commande + " WHERE nom_recette = '" + nom_recette + "';";
            command.CommandText = requete;
            reader = command.ExecuteReader();
            reader.Close();

            if (nbr_commande < 11 && nv_nbr_commande >= 11)
            {
                requete = "SELECT prix_recette FROM recette WHERE nom_recette = '" + nom_recette + "';";
                command.CommandText = requete;
                reader = command.ExecuteReader();
                reader.Read();
                int prix_recette = 2 + int.Parse(reader.GetValue(0).ToString());
                reader.Close();

                requete = "UPDATE recette SET prix_recette =" + prix_recette + " WHERE nom_recette = '" + nom_recette + "';";
                command.CommandText = requete;
                reader = command.ExecuteReader();
                reader.Close();
            }
            if (nbr_commande < 51 && nv_nbr_commande >= 51)
            {
                requete = "SELECT prix_recette FROM recette WHERE nom_recette = '" + nom_recette + "';";
                command.CommandText = requete;
                reader = command.ExecuteReader();
                reader.Read();
                int prix_recette = 5 + int.Parse(reader.GetValue(0).ToString());
                reader.Close();

                requete = "UPDATE recette SET prix_recette =" + prix_recette + " WHERE nom_recette = '" + nom_recette + "';";
                command.CommandText = requete;
                reader = command.ExecuteReader();
                reader.Close();

                requete = "SELECT remuneration_cdr FROM recette WHERE nom_recette = '" + nom_recette + "';";
                command.CommandText = requete;
                reader = command.ExecuteReader();
                reader.Read();
                int remuneration_cdr = 4 + int.Parse(reader.GetValue(0).ToString());
                reader.Close();

                requete = "UPDATE recette SET remuneration_cdr =" + remuneration_cdr + " WHERE nom_recette = '" + nom_recette + "';";
                command.CommandText = requete;
                reader = command.ExecuteReader();
                reader.Close();
            }

            maConnexion.Close();
        }

        static void MiseAJourCooks(MySqlConnection maConnexion, string numtel_client, int difference_de_cooks)
        {
            maConnexion.Open();

            string requete = "SELECT nombre_cook FROM client WHERE numtel_client = '" + numtel_client + "';";
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            int nouveau_solde = int.Parse(reader.GetValue(0).ToString()) + difference_de_cooks;
            reader.Close();

            requete = "UPDATE client SET nombre_cook ="+ nouveau_solde + " WHERE numtel_client = '"+ numtel_client + "';";
            command.CommandText = requete;
            reader = command.ExecuteReader();
            reader.Close();
            maConnexion.Close();
        }

        static void BannirCdR(MySqlConnection maConnexion, string numtel_client)
        {
            maConnexion.Open();

            string requete = "UPDATE client SET nbr_recettes_crees = -1 WHERE numtel_client = '" + numtel_client + "';";
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;
            MySqlDataReader reader = command.ExecuteReader();
            reader.Close();
            maConnexion.Close();
        }

        static void DébiterClient(MySqlConnection maConnexion, string numtel_client, string nom_recette, int nombre)
        {
            maConnexion.Open();

            string requete = "SELECT prix_recette FROM recette WHERE nom_recette = '" + nom_recette + "';";
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            int prix_recette = 0 - (nombre * int.Parse(reader.GetValue(0).ToString()));
            reader.Close();
            maConnexion.Close();
            MiseAJourCooks(maConnexion, numtel_client, prix_recette);
        }

        static void PayerCdR(MySqlConnection maConnexion, string nom_recette, int nombre)
        {
            maConnexion.Open();

            string requete = "SELECT remuneration_cdr, numtel_client FROM recette WHERE nom_recette = '" + nom_recette + "';";
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            int remuneration = (nombre * int.Parse(reader.GetValue(0).ToString()));
            string numtel_client = reader.GetValue(1).ToString();
            reader.Close();
            maConnexion.Close();
            MiseAJourCooks(maConnexion, numtel_client, remuneration);
        }

        static List<string[]> ListeRecette(MySqlConnection maConnexion, int type_recette, int num_page, string numtel_client)//Si on veut afficher lors de commande on emts num tel null, et si on veut afficher les recettes d'un cdr on mets num et le type est useless
        {
            // Type recette : 0 = ALL, 1 = Entree, 2 = Plat, 3 = Dessert, 4 = Boisson
            string where = "";
            if (numtel_client != null)
            {
                where = "WHERE numtel_client = '"+ numtel_client + "'";
            }
            else
            {
                switch (type_recette)
                {
                    case 1:
                        where = "WHERE type_recette = 'Entrée'";
                        break;
                    case 2:
                        where = "WHERE type_recette = 'Plat'";
                        break;
                    case 3:
                        where = "WHERE type_recette = 'Dessert'";
                        break;
                    case 4:
                        where = "WHERE type_recette = 'Boisson'";
                        break;
                }
            }
            
            maConnexion.Open();
            string requete = "SELECT nom_recette, descriptif_recette, prix_recette, nbr_commande, date_creation, remuneration_cdr FROM recette " + where + " GROUP BY nom_recette ORDER BY nbr_commande DESC;";
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;
            MySqlDataReader reader = command.ExecuteReader();
            List<string[]> toutes_les_recettes = new List<string[]>();
            while(reader.Read())
            {
                toutes_les_recettes.Add(new string[] { reader.GetValue(0).ToString(), reader.GetValue(1).ToString(), reader.GetValue(2).ToString(), reader.GetValue(3).ToString(), reader.GetValue(4).ToString(), reader.GetValue(5).ToString() });
            }
            List<string[]> recettes_a_afficher = new List<string[]>();
            int maxboucle = num_page * 6 + 6;
            if (num_page * 6 + 6 > toutes_les_recettes.Count)
            {
                maxboucle = toutes_les_recettes.Count;
            }
            for (int i = num_page * 6; i < maxboucle; i++)
            {
                recettes_a_afficher.Add(toutes_les_recettes[i]);
            }
            reader.Close();
            maConnexion.Close();
            return recettes_a_afficher;
        }

        static bool CheckNbIngrédients(MySqlConnection maConnexion, string nom_recette, int nombre)
        {
            maConnexion.Open();
            bool a_assez_dingredients = true;
            string requete = "SELECT nom_produit, nombre_necessaire FROM utilise WHERE nom_recette = '" + nom_recette + "';";
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;
            MySqlDataReader reader = command.ExecuteReader();
            List<string[]> produitsnecessaires = new List<string[]>();
            while (reader.Read())
            {
                produitsnecessaires.Add(new string[] { reader.GetValue(0).ToString(), reader.GetValue(1).ToString() });
            }
            reader.Close();

            for (int i = 0; i< produitsnecessaires.Count; i++)
            {
                requete = "SELECT stock_actuel FROM produit WHERE nom_produit = '" + produitsnecessaires[i][0] + "';";
                command.CommandText = requete;
                reader = command.ExecuteReader();
                reader.Read();
                if (int.Parse(reader.GetValue(0).ToString())< nombre * Convert.ToInt32(produitsnecessaires[i][1]))
                {
                    a_assez_dingredients = false;
                }
                reader.Close();
            }
            maConnexion.Close();
            return a_assez_dingredients;
        }

        static bool CheckMail(MySqlConnection maConnexion, string mail)
        {
            maConnexion.Open();
            bool mail_valide = true;
            string requete = "SELECT mail_client FROM client WHERE mail_client IS NOT null;";
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;
            MySqlDataReader reader = command.ExecuteReader();
            List<string> mailexistant = new List<string>();
            while (reader.Read())
            {
                mailexistant.Add(reader.GetValue(0).ToString());
            }
            reader.Close();

            for (int i = 0; i < mailexistant.Count; i++)
            {
                if (mailexistant[i] == mail)
                {
                    mail_valide = false;
                }
            }
            maConnexion.Close();
            return mail_valide;
        }

        static bool CheckTel(MySqlConnection maConnexion, string tel)
        {
            maConnexion.Open();
            bool tel_valide = true;
            string requete = "SELECT numtel_client FROM client;";
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;
            MySqlDataReader reader = command.ExecuteReader();
            List<string> telexistant = new List<string>();
            while (reader.Read())
            {
                telexistant.Add(reader.GetValue(0).ToString());
            }
            reader.Close();

            for (int i = 0; i < telexistant.Count; i++)
            {
                if (telexistant[i] == tel)
                {
                    tel_valide = false;
                }
            }
            maConnexion.Close();
            return tel_valide;
        }

        static int NombreCooks(MySqlConnection maConnexion, string numtel_client)
        {
            maConnexion.Open();
            string requete = "SELECT nombre_cook FROM client WHERE numtel_client = '"+ numtel_client + "';";
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            int nombre_cooks = int.Parse(reader.GetValue(0).ToString());
            reader.Close();
            maConnexion.Close();
            return nombre_cooks;
        }

        static bool CheckCookClient(MySqlConnection maConnexion, string numtel_client, string nom_recette, int nombre)
        {
            int nombre_cooks_client = NombreCooks(maConnexion, numtel_client);
            maConnexion.Open();
            bool a_assez_desousous = true;
            string requete = "SELECT prix_recette FROM recette WHERE nom_recette = '" + nom_recette + "';";
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            if(nombre_cooks_client < nombre * int.Parse(reader.GetValue(0).ToString()))
            {
                a_assez_desousous = false;
            }
            
            reader.Close();
            maConnexion.Close();
            return a_assez_desousous;
        }

        static void SupprimerRecette(MySqlConnection maConnexion, string nom_recette_ou_tel_client, bool suppr_client) // Si on suppr qu'une recette on envoie le nom et false, sinon on envoie le num client
        {
            maConnexion.Open();
            string where = "";
            if (suppr_client == true)
            {
                where = "WHERE numtel_client = '" + nom_recette_ou_tel_client+"'";
            }
            else
            {
                where = "WHERE nom_recette = '" + nom_recette_ou_tel_client + "'";
            }
            string requete = "DELETE FROM recette "+where+";";
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;
            MySqlDataReader reader = command.ExecuteReader();
            reader.Close();
            maConnexion.Close();
        }

        static List<string[]> CdrDor(MySqlConnection maConnexion, bool semaine)
        {
            // Type recette : 0 = ALL, 1 = Entree, 2 = Plat, 3 = Dessert, 4 = Boisson
            string where = "";
            if (semaine ==true)
            {
                DateTime date_ajd = DateTime.Now;
                int delta = DayOfWeek.Monday - date_ajd.DayOfWeek;
                DateTime monday = date_ajd.AddDays(delta);
                string monday_sql = monday.ToString("yyyy-MM-dd 00:00:00");
                Console.WriteLine(monday_sql);
                where = "AND com.date_commande >= '" + monday_sql + "'";
            }

            maConnexion.Open();
            string requete = "SELECT cli.numtel_client, SUM(comp.nombre_recette_dans_commande) somme FROM client cli, compose comp, commande com, recette rec WHERE cli.numtel_client = rec.numtel_client AND com.num_commande = comp.num_commande AND rec.nom_recette = comp.nom_recette " + where + " GROUP BY cli.numtel_client ORDER BY somme DESC;";
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;
            MySqlDataReader reader = command.ExecuteReader();
            List<string[]> top = new List<string[]>();
            int i = 0;
            while(reader.Read() && i<5)
            {
                top.Add(new string[] { reader.GetValue(0).ToString(), reader.GetValue(1).ToString() });
                i++;
            }
            
            reader.Close();
            maConnexion.Close();
            return top;
        }

        static List<string[]> TopRecette(MySqlConnection maConnexion, bool semaine)
        {
            // Type recette : 0 = ALL, 1 = Entree, 2 = Plat, 3 = Dessert, 4 = Boisson
            string where = "";
            if (semaine == true)
            {
                DateTime date_ajd = DateTime.Now;
                int delta = DayOfWeek.Monday - date_ajd.DayOfWeek;
                DateTime monday = date_ajd.AddDays(delta);
                string monday_sql = monday.ToString("yyyy-MM-dd 00:00:00");
                where = "AND com.date_commande >= '" + monday_sql + "'";
            }

            maConnexion.Open();
            string requete = "SELECT rec.nom_recette, SUM(comp.nombre_recette_dans_commande) somme FROM compose comp, commande com, recette rec WHERE com.num_commande = comp.num_commande AND rec.nom_recette = comp.nom_recette " + where + " GROUP BY rec.nom_recette ORDER BY somme DESC;";
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;
            MySqlDataReader reader = command.ExecuteReader();
            List<string[]> top = new List<string[]>();
            int i = 0;
            while (reader.Read() && i < 5)
            {
                top.Add(new string[] { reader.GetValue(0).ToString(), reader.GetValue(1).ToString() });
                i++;
            }

            reader.Close();
            maConnexion.Close();
            return top;
        }

        static List<string[]> Liste_Historique(MySqlConnection maConnexion, int num_page, string numtel_client)
        {
            maConnexion.Open();
            string requete = "SELECT distinct(com.num_commande) FROM compose comp, commande com WHERE com.num_commande = comp.num_commande AND com.num_tel_client = '" + numtel_client + "' ORDER BY com.num_commande DESC;";

            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;
            MySqlDataReader reader = command.ExecuteReader();
            List<string[]> toutes_les_recettes = new List<string[]>();
            while (reader.Read())
            {
                toutes_les_recettes.Add(new string[] { reader.GetValue(0).ToString() });
            }
            List<string[]> recettes_a_afficher = new List<string[]>();
            int maxboucle = num_page * 6 + 6;
            if (num_page * 6 + 6 > toutes_les_recettes.Count)
            {
                maxboucle = toutes_les_recettes.Count;
            }
            for (int i = num_page * 6; i < maxboucle; i++)
            {
                recettes_a_afficher.Add(toutes_les_recettes[i]);
            }
            reader.Close();
            maConnexion.Close();
            return recettes_a_afficher;
        }

        static List<string[]> Detail_Commande(MySqlConnection maConnexion, int num_commande)
        {
            //Retourne une liste avec en premier un tableau avec les divers infos, puis les autres cases sont des tableaux avec les recettes commandées
            maConnexion.Open();
            string requete = "SELECT com.num_commande, com.prix_commande, com.date_commande FROM commande com WHERE com.num_commande = '" + num_commande + "';";

            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;
            MySqlDataReader reader = command.ExecuteReader();
            List<string[]> detail_commande = new List<string[]>();
            reader.Read();
            detail_commande.Add(new string[] { reader.GetValue(0).ToString(), reader.GetValue(1).ToString(), reader.GetValue(2).ToString() });
            reader.Close();


            requete = "SELECT comp.nom_recette, comp.nombre_recette_dans_commande FROM compose comp, commande com WHERE com.num_commande = comp.num_commande AND com.num_commande = '" + num_commande + "';";
            command.CommandText = requete;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                detail_commande.Add(new string[] { reader.GetValue(0).ToString(), reader.GetValue(1).ToString() });
            }


            maConnexion.Close();
            return detail_commande;

        }





        
            
        
    }
}
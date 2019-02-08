using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using LiveCharts;
using LiveCharts.Wpf;

namespace LeagueDataViewer
{
    public partial class Champion : MaterialForm
    {
        public static string championName;
        public Champion(string ChampionName)
        {
            InitializeComponent();
            var skinManager = MaterialSkin.MaterialSkinManager.Instance;
            skinManager.AddFormToManage(this);
            skinManager.Theme = MaterialSkin.MaterialSkinManager.Themes.DARK;
            skinManager.ColorScheme = new MaterialSkin.ColorScheme
                (Primary.Red800, Primary.Red900, Primary.Red900, Accent.Red700, TextShade.BLACK);
            championName = ChampionName;
            LoadPicture();
            heroNameLabel.Text = championName;
            heroNameLabel.ForeColor = System.Drawing.Color.White;
            string ChampionKey = GetKeyOfChampion();
            LoadChampionStaticData();
            LoadChampionData(ChampionKey);
            logoPic.ImageLocation = "./images/riot.png"; //path to image
            logoPic.BackColor = Color.Transparent;
        }
        public void LoadChampionData(string ChampionKey)
        {
            int winRate = 0;
            int lostRate = 0;
            long magicDamage = 0;
            long trueDamage = 0;
            long physDamage = 0;
            int banCount = 0;
            int killCount = 0;
            int assistCount = 0;
            int deathCount = 0;
            using (StreamReader r = new StreamReader("./StaticData/statistics.json"))
            {
                string json = r.ReadToEnd();
                List<ChampionData> statistics = JsonConvert.DeserializeObject<List<ChampionData>>(json);
                
                foreach(var s in statistics)
                {
                    if (Int32.Parse(ChampionKey) == s.key)
                    {
                        winRate = s.winCount;
                        lostRate = s.lossCount;
                        magicDamage = Convert.ToInt64(s.magicDmg["$numberLong"]);
                        trueDamage = Convert.ToInt64(s.trueDmg["$numberLong"]);
                        physDamage = Convert.ToInt64(s.physDmg["$numberLong"]);
                        banCount = s.banCount;
                        killCount = s.killCount;
                        assistCount = s.assistCount;
                        deathCount = s.deathCount;
                    }
                }

            }
            int totalGames = winRate + lostRate;
            createPickBanDiagram(totalGames,banCount);
            createPieDiagram(totalGames,winRate,lostRate);
            createDamageDiagram(magicDamage,trueDamage,physDamage);
            createKDADiagram(totalGames,killCount,assistCount,deathCount);
        }
        public void createKDADiagram(int games, int kills, int assists, int deaths)
        {
            double averagekills = Math.Round(Convert.ToDouble(kills) / games,2);
            double averageassists = Math.Round(Convert.ToDouble(assists) / games,2);
            double averagedeaths = Math.Round(Convert.ToDouble(deaths) / games,2);
            chartKDA.Series["Series1"].Points.Add(averagekills);
            chartKDA.Series["Series1"].Points[0].Color = Color.DarkOliveGreen;
            chartKDA.Series["Series1"].Points[0].Label = $"{averagekills}";
            chartKDA.Series["Series1"].Points[0].AxisLabel = "Kills";
            chartKDA.Series["Series1"].Points.Add(averageassists);
            chartKDA.Series["Series1"].Points[1].Color = Color.MediumBlue;
            chartKDA.Series["Series1"].Points[1].Label = $"{averageassists}";
            chartKDA.Series["Series1"].Points[1].AxisLabel = "Assists";
            chartKDA.Series["Series1"].Points.Add(averagedeaths);
            chartKDA.Series["Series1"].Points[2].Color = Color.MediumPurple;
            chartKDA.Series["Series1"].Points[2].Label = $"{averagedeaths}";
            chartKDA.Series["Series1"].Points[2].AxisLabel = "Deaths";
            double kdaRatio = Math.Round(Convert.ToDouble(kills+assists) / deaths, 2);
            kdaRatioText.Text = kdaRatio.ToString("0.00");
        }
        public void createPickBanDiagram(int pickCount,int banCount)
        {
            int games = 9812;
            double pickPercent= Math.Round((Convert.ToDouble(pickCount) / games) * 100.0, 2);
            double banPercent = Math.Round((Convert.ToDouble(banCount) / games) * 100.0, 2);
            chartPickBan.Series["Series1"].Points.Add(pickPercent);
            chartPickBan.Series["Series1"].Points[0].Color = Color.DarkOliveGreen;
            chartPickBan.Series["Series1"].Points[0].Label = $"{pickPercent}%";
            chartPickBan.Series["Series1"].Points[0].AxisLabel = "Pick";
            chartPickBan.Series["Series1"].Points.Add(banPercent);
            chartPickBan.Series["Series1"].Points[1].Color = Color.BlueViolet;
            chartPickBan.Series["Series1"].Points[1].Label = $"{banPercent}%";
            chartPickBan.Series["Series1"].Points[1].AxisLabel = "Ban";

        }
        public void createDamageDiagram(long magicDmg,long trueDmg, long physDmg)
        {
            long totalDamage = magicDmg + trueDmg + physDmg;
            double magicPercent =Math.Round((Convert.ToDouble(magicDmg) / totalDamage) * 100.0, 2);
            double truePercent = Math.Round((Convert.ToDouble(trueDmg) / totalDamage) * 100.0, 2);
            double physPercent = Math.Round((Convert.ToDouble(physDmg) / totalDamage) * 100.0, 2);
            damageDealt.Series["Damage Dealt"].Points.Add(magicPercent);
            damageDealt.Series["Damage Dealt"].Points[0].Color = Color.Plum;
            damageDealt.Series["Damage Dealt"].Points[0].Label = $"{magicPercent}%";
            damageDealt.Series["Damage Dealt"].Points[0].AxisLabel = "Magic"; 
            damageDealt.Series["Damage Dealt"].Points.Add(physPercent);
            damageDealt.Series["Damage Dealt"].Points[1].Color = Color.Purple;
            damageDealt.Series["Damage Dealt"].Points[1].Label = $"{physPercent}%";
            damageDealt.Series["Damage Dealt"].Points[1].AxisLabel = "Physical";
            damageDealt.Series["Damage Dealt"].Points.Add(truePercent);
            damageDealt.Series["Damage Dealt"].Points[2].Color = Color.Sienna;
            damageDealt.Series["Damage Dealt"].Points[2].Label = $"{truePercent}%";
            damageDealt.Series["Damage Dealt"].Points[2].AxisLabel = "True";

        }
        public void createPieDiagram(int totalGames, int winRate, int lostRate)
        {
           double winPercent = Math.Round((Convert.ToDouble(winRate) / totalGames) * 100.0, 2);
           double lostPercent= Math.Round((Convert.ToDouble(lostRate) / totalGames) * 100.0, 2);

            chartWinBan.Series["Series1"].Points.Add(winPercent);
            chartWinBan.Series["Series1"].Points[0].Label = $"Win {winPercent}%";
            chartWinBan.Series["Series1"].Points.Add(lostPercent);
            chartWinBan.Series["Series1"].Points[1].Label = $"Loss {lostPercent}%";
        }
        public void LoadChampionStaticData()
        {
        healthPc.ImageLocation = $"./images/statsImages/Health.png";
        healthPc.BackColor = Color.Transparent;
        magicRezPc.ImageLocation = $"./images/statsImages/MagicRezist.png";
        magicRezPc.BackColor = Color.Transparent;
        armorPc.ImageLocation = $"./images/statsImages/Armor.png";
        armorPc.BackColor = Color.Transparent;
        damage.ImageLocation = $"./images/statsImages/Damage.png";
        damage.BackColor = Color.Transparent;
        atSpeedPc.ImageLocation = $"./images/statsImages/AttackSpeed.png";
        atSpeedPc.BackColor = Color.Transparent;
        JObject json;
        using (StreamReader file = File.OpenText("./StaticData/champion.json"))
        using (JsonTextReader reader = new JsonTextReader(file))
        {
            json = (JObject)JToken.ReadFrom(reader);
        }
        healthTxt.Text = (string)json["data"][championName]["stats"]["hp"];
        magicRezTxt.Text = (string)json["data"][championName]["stats"]["spellblock"];
        armorTxt.Text = (string)json["data"][championName]["stats"]["armor"];
        damageTxt.Text = (string)json["data"][championName]["stats"]["attackdamage"];
        atSpeedTxt.Text = (string)json["data"][championName]["stats"]["attackspeed"];
        }
        public void LoadPicture()
        {
            backPicture.ImageLocation = $"./images/homebtn.png";
            backPicture.BackColor = Color.Transparent;
            heroPicture.ImageLocation = $"./images/champions/{championName}.png"; //path to image
            heroPicture.BackColor = Color.Transparent;

        }
        public string GetKeyOfChampion()
        {
            JObject json;
            using (StreamReader file = File.OpenText("./StaticData/champion.json"))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                json = (JObject)JToken.ReadFrom(reader);
            }
            string key = (string)json["data"][championName]["key"];
            return key;
        }

        private void backPicture_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.ShowDialog();
        }
    }

    public class ChampionData
    {
        public dynamic _id { get; set; }
        public int assistCount { get; set; }
        public int banCount { get; set; }
        public int deathCount { get; set; }
        public dynamic gameTime { get; set; }
        public int key { get; set; }
        public int killCount { get; set; }
        public int lossCount { get; set; }
        public dynamic magicDmg { get; set; }
        public dynamic physDmg { get; set; }
        public int spell1Count { get; set; }
        public int spell2Count { get; set; }
        public dynamic trueDmg { get; set; }
        public int winCount { get; set; }

        public ChampionData(dynamic id, int assist, int ban, int death, dynamic time, int k, int kill, int loss, dynamic magic, dynamic phys, int spell1,int spell2,dynamic truedmg, int win)
        {
            _id= id;
            assistCount = assist;
            banCount=ban;
            deathCount = death;
            gameTime = time;
            key = k;
            killCount = kill;
            lossCount = loss;
            magicDmg = magic;
            physDmg = phys;
            spell1Count = spell1;
            spell2Count = spell2;
            trueDmg = truedmg;
            winCount = win;
        }

    }
}

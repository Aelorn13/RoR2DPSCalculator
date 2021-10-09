using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoR2DPSCalculator
{
    public partial class Form1 : Form
    {
        RoRCharacter MyHero = new RoRCharacter(" Commando", 110, 33, 1, 0.2, 12, 2.4, 7, 0, 1,6,0);
        public Form1()
        {
            InitializeComponent();
        }

        bool HeroChanged;
        private string ShowStats()
        {
            if (MyHero.Name == " Commando")
            pictureBox1.Image = Properties.Resources.Commando;
            else if (MyHero.Name == " REX")
                pictureBox1.Image = Properties.Resources.REX;
            else if (MyHero.Name == " Huntress")
                pictureBox1.Image = Properties.Resources.Huntress;

           string text = MyHero.Name  + "\n Health: " + MyHero.Health + "\n Health Regeneration: " + MyHero.HealthRegen.ToString("#.0") + "\n Damage: " + MyHero.Damage.ToString("#.0") + "\n Armor: " + MyHero.Armor + "\n Speed: " + MyHero.Speed + "\n Attacks per second: " + MyHero.Attack_speed + "\n Level: " + MyHero.Level;
            return text;
        }
        private string ShowStatsWithItems()
        {
            string text = MyHero.Name + "\n Health: " + (MyHero.Health+ItModBison*25) + "\n Health Regeneration: " + (MyHero.HealthRegen+ItModCauSlus*3).ToString("#.0") + "\n Damage: " + (MyHero.Damage+ MyHero.Damage*(ItModArmorRounds * 0.2)).ToString("#.0") + "\n Armor: " + (MyHero.Armor+ItModBuckler*30) + "\n Speed: " + (MyHero.Speed+MyHero.Speed*(ItModPaulGoat*0.14)).ToString("#.0") + "\n Attacks per second: " + (MyHero.Attack_speed+ MyHero.Attack_speed*(ItModSoldier*0.15)).ToString("#.0") + "\n Level: " + MyHero.Level;
            return text;
        }
        int ItModArmorRounds = 0, ItModBison = 0, ItModCauSlus = 0, ItModPerforator = 0, ItModFocusCrystal = 0, ItModPaulGoat = 0, ItModSoldier = 0, ItModBomb = 0, ItModAtG = 0, ItModScythe = 0, ItModGlasses = 0, ItModBuckler = 0;
        bool show=true;
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (show)
            {
                pictureBox2.Show();
                label6.Show();
                show = false;
            }
            if (listBox1.Items[listBox1.SelectedIndex].ToString() == "Armor-Piercing Rounds")
            {
                pictureBox2.Image = Properties.Resources.ArmorRounds;
                label6.Text = "Deal an additional 20% damage (+20% per stack)";
            }
            else if (listBox1.Items[listBox1.SelectedIndex].ToString() == "Bison Steak")
            {
                pictureBox2.Image = Properties.Resources.Bison_Steak;
                label6.Text = "Increases maximum health by 25 (+25 per stack).";
            }
            else if (listBox1.Items[listBox1.SelectedIndex].ToString() == "Cautious Slug")
            {
                pictureBox2.Image = Properties.Resources.Cautious_Slug;
                label6.Text = "Increases base health regeneration by +3 hp/s (+3 hp/s per stack) while outside of combat.";
            }
            else if (listBox1.Items[listBox1.SelectedIndex].ToString() == "Charged Perforator")
            {
                pictureBox2.Image = Properties.Resources.Charged_Perforator;
                label6.Text = "10% chance on hit to down a lightning strike, dealing 500% (+500% per stack) damage.";
            }
            else if (listBox1.Items[listBox1.SelectedIndex].ToString() == "Focus Crystal")
            {
                pictureBox2.Image = Properties.Resources.Focus_Crystal;
                label6.Text = "Increase damage to enemies within 13m by 20% (+20% per stack).";
            }
            else if (listBox1.Items[listBox1.SelectedIndex].ToString() == "Lens-Maker's Glasses")
            {
                pictureBox2.Image = Properties.Resources.Glasses;
                label6.Text = "Your attacks have a 10% (+10% per stack) chance to 'Critically Strike', dealing double damage.";
            }
            else if (listBox1.Items[listBox1.SelectedIndex].ToString() == "Paul's Goat Hoof")
            {
                pictureBox2.Image = Properties.Resources.Paul;
                label6.Text = "Increases movement speed by 14% (+14% per stack).";
            }
            else if (listBox1.Items[listBox1.SelectedIndex].ToString() == "Soldier's Syringe")
            {
                pictureBox2.Image = Properties.Resources.Soldier_Syringe;
                label6.Text = "Increases attack speed by 15% (+15% per stack).";
            }
            else if (listBox1.Items[listBox1.SelectedIndex].ToString() == "Sticky Bomb")
            {
                pictureBox2.Image = Properties.Resources.Sticky_Bomb;
                label6.Text = "5% (+5% per stack) chance on hit to attach a bomb to an enemy, detonating for 180% TOTAL damage.";
            }
            else if (listBox1.Items[listBox1.SelectedIndex].ToString() == "AtG Missile Mk. 1")
            {
                pictureBox2.Image = Properties.Resources.AtG;
                label6.Text = "10% chance to fire a missile that deals 300% (+300% per stack) TOTAL damage.";
            }
            else if (listBox1.Items[listBox1.SelectedIndex].ToString() == "Harvester's Scythe")
            {
                pictureBox2.Image = Properties.Resources.Harvester_Scythe;
                label6.Text = "Gain 5% (+5% per stack) critical chance. Critical strikes heal for 8 (+4 per stack) health.";
            }
            else if (listBox1.Items[listBox1.SelectedIndex].ToString() == "Rose Buckler")
            {
                pictureBox2.Image = Properties.Resources.RoseBuckler;
                label6.Text = "Increase armor by 30 (+30 per stack)";
            }
        }
        private double DPS(RoRCharacter MyHero)
        {        
           int critGlobal;
            int FocusActivated;
            Random rand= new Random();
            double DPS = 0;

            if (Convert.ToInt32(numericUpDown2.Value) <= 13) { FocusActivated = 1; } else { FocusActivated = 0; }//Check Distance for crystal
            for (int i = 0; i < (MyHero.Attack_speed + (ItModSoldier * 0.15)) * 100; i++)
            {
                if (rand.NextDouble() < ((0.1 * ItModGlasses + 0.05 * ItModScythe) * MyHero.Proc_coef)) { critGlobal = 2; } else { critGlobal = 1; } //Is hit - CRIT?


                DPS += critGlobal * (MyHero.Attack(Convert.ToInt32(numericUpDown2.Value)) + MyHero.Damage * (ItModArmorRounds * 0.2) +MyHero.Damage * FocusActivated * (ItModFocusCrystal * 0.2)); //Just attack calculation


                if (rand.NextDouble() < 0.1 * MyHero.Proc_coef&& ItModPerforator>=1) //PerfProc
                    {
                    DPS += critGlobal * (MyHero.Damage * 5 * ItModPerforator);
                    }
                if (rand.NextDouble() < 0.1 * MyHero.Proc_coef&& ItModAtG>=1)//AtgProc
                    {
                    DPS += critGlobal * (MyHero.Attack(Convert.ToInt32(numericUpDown2.Value)) + MyHero.Damage * (ItModArmorRounds * 0.2) + MyHero.Damage * FocusActivated * (ItModFocusCrystal * 0.2)) * (3 * ItModAtG);
                    }
                if (rand.NextDouble() < (0.05 * ItModBomb) * MyHero.Proc_coef&&ItModBomb>=1)//BombProc
                    {
                    DPS += critGlobal * (MyHero.Attack(Convert.ToInt32(numericUpDown2.Value)) + MyHero.Damage * (ItModArmorRounds * 0.2) + MyHero.Damage * FocusActivated * (ItModFocusCrystal * 0.2)) * 1.8;
                    }
                
               
                label11.Text = (MyHero.Attack(Convert.ToInt32(numericUpDown2.Value)) + MyHero.Damage * (ItModArmorRounds * 0.2) +MyHero.Damage * FocusActivated * (ItModFocusCrystal * 0.2)).ToString();
            }
            return DPS/100;
        }
        private void CountItems ()
        {
            ItModArmorRounds = 0; ItModBison = 0; ItModCauSlus = 0; ItModPerforator = 0; ItModFocusCrystal = 0; ItModPaulGoat = 0; ItModSoldier = 0; ItModBomb = 0; ItModAtG = 0; ItModScythe = 0; ItModGlasses = 0; ItModBuckler = 0;
            for (int i = 0; i < listBox2.Items.Count; i++)
            {
                if (listBox2.Items[i].ToString() == "Armor-Piercing Rounds")
                {
                    ItModArmorRounds++;
                 //   label6.Text = ItModArmorRounds.ToString();
                }
                else if (listBox2.Items[i].ToString() == "Bison Steak")
                {
                    ItModBison++;
                  
                }
                else if (listBox2.Items[i].ToString() == "Cautious Slug")
                {
                    ItModCauSlus++;
                }
                else if (listBox2.Items[i].ToString() == "Charged Perforator")
                {
                    ItModPerforator++;
                }
                else if (listBox2.Items[i].ToString() == "Focus Crystal")
                {
                    ItModFocusCrystal++;
                }
                else if (listBox2.Items[i].ToString() == "Lens-Maker's Glasses")
                {
                    ItModGlasses++;
                }
                else if (listBox2.Items[i].ToString() == "Paul's Goat Hoof")
                {
                    ItModPaulGoat++;
                }
                else if (listBox2.Items[i].ToString() == "Soldier's Syringe")
                {
                    ItModSoldier++;
                }
                else if (listBox2.Items[i].ToString() == "Sticky Bomb")
                {
                    ItModBomb++;
                }
                else if (listBox2.Items[i].ToString() == "AtG Missile Mk. 1")
                {
                    ItModAtG++;
                }
                else if (listBox2.Items[i].ToString() == "Harvester's Scythe")
                {
                    ItModScythe++;
                }
                else if (listBox2.Items[i].ToString() == "Rose Buckler")
                {
                    ItModBuckler++;
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex=0;
            label3.Text = ShowStats();
            label2.Text = ShowStatsWithItems();
            pictureBox2.Hide();
            label6.Hide();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            if (MyHero.Level + Convert.ToInt32(numericUpDown1.Value) > 0)
            {
                MyHero.LevelUp(Convert.ToInt32(numericUpDown1.Value));
                label3.Text = ShowStats();
                label2.Text = ShowStatsWithItems();

            }
            else
            {
                    MessageBox.Show("Character Level can not be less than 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);               
            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {           
           string heroName = Convert.ToString(comboBox1.SelectedItem);

            if (heroName == "REX")
            {
                this.MyHero = new RoRCharacter(" REX", 130, 39, 1, 0.2, 12, 2.4, 7, 20, 0.5,1,0);
                label3.Text = ShowStats();
                HeroChanged = true;
            }
            else if (heroName == "Huntress")
            {
                this.MyHero = new RoRCharacter(" Huntress", 90, 27, 1, 0.2, 12, 2.4, 7, 0,1 ,2,0);
                label3.Text = ShowStats();
                HeroChanged = true;
            }
            else if (heroName=="Commando") 
            {
               this.MyHero = new RoRCharacter(" Commando", 110, 33, 1, 0.2, 12, 2.4, 7, 0, 1, 6, 0);
                label3.Text = ShowStats();
                HeroChanged = true;
            }
            else
            {
                label3.Text = "";
            }
        }

        private void numericUpDown1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (MyHero.Level + Convert.ToInt32(numericUpDown1.Value) > 0)
                {
                    MyHero.LevelUp(Convert.ToInt32(numericUpDown1.Value));
                    label2.Text = (Convert.ToString(numericUpDown1.Value));
                    label3.Text = ShowStats();
                }
                else
                {
                    MessageBox.Show("Character Level can not be less than 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        Double MaxEnemyHp = 10000;
        double EnemyHp = 10000;
        private void timer1_Tick(object sender, EventArgs e) // переделать в текстовую версию? Вообще убрать?
        {
            MaxEnemyHp = Convert.ToInt32(numericUpDown3.Value);
            Random rand = new Random();
            double RoughStep;
            if (progressBar1.Value == progressBar1.Maximum||EnemyHp<=0) {progressBar1.Value = 0; EnemyHp = MaxEnemyHp; } //update for next enemy
            label7.Text = DPS(MyHero).ToString("#.0");                                                      
            RoughStep = (DPS(MyHero) / MaxEnemyHp) * 100;
            progressBar1.Step=Convert.ToInt32(RoughStep);
            progressBar1.PerformStep();
            EnemyHp -= DPS(MyHero);
            label11.Text = "Current enemy health points: "+EnemyHp.ToString("#.0");

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }
        private void button2_Click(object sender, EventArgs e) //Add item
        {
            try
            {      
                label3.Text = ShowStats();
                listBox2.Items.Add(listBox1.Items[listBox1.SelectedIndex].ToString());
                CountItems();
                label2.Text = ShowStatsWithItems();
            }
            catch
            {
                MessageBox.Show("Choose which item should be added", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)//Remove item
        {
            try
            {
                listBox2.Items.Remove(listBox2.Items[listBox2.SelectedIndex].ToString());
                CountItems();
                label2.Text = ShowStatsWithItems();
            }
            catch
            {
                MessageBox.Show("Choose which item should be removed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

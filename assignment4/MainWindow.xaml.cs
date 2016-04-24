using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace assignment4
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        int minsup;
        //path masire file csv ast
        string path = "";

        //classe node shamele yek maj-mue va tedade tekrare an dar dataset mibashad
        //masalan {a} 2 ya {a,b} 3. ke baraye negah dashtane maj-mue az list estefade kardam
        public class node
        {
            public List<string> ozv;
            public int count { get; set; }

            public node()
            {
                ozv=new List<string>();
                count = 0;
            }

            public override string ToString()
            {
                string s = "{";
                int i;
                for (i = 0; i < ozv.Count-1; i++)
                {
                    s +=ozv[i]  + ',';
                }
                s += ozv[i];
                s += '}';
                return s;
            }
        }

        //sourcelist haman datasete vurudi ast,ke vurudi ra khandam va dar an rikhte-am
        //sourcelist listi az liste string ast
        //har khat az vurdi ra(fagat maj-mue haye har tarakonesh(Itemset ha)) be surate listi az a-za joda kardam va dar sourcelist add kardam
        List<List<string>> sourceList = new List<List<string>>();

        //ActiveFrequent haman listi ast ke har daf-e ferequent ra hesab mikonam dar an mirizam
        public List<node> ActiveFrequent = new List<node>();

        
        //vagti button entekhab ra mizanim dar UI,varede in tabe mishavim
        private void Select_path(object sender, RoutedEventArgs e)
        {
            //khandane masire file dataset(*.csv) va rikhtane an dar path
            OpenFileDialog op=new OpenFileDialog();
            if (op.ShowDialog() == true)
            {
                path = op.FileName;
            }
            Label.Content = "";

        }

        //step : marahaleye ijade jadvale frequent ra negah midaram da an
        int step = 1;

        //tabe-e CF yek list az node ra migirad va ebteda jadvale C ra ijad mikonad sepas anhaii ra ke kamtar az min_support hastand
        //ra hazf mikonad va jadvale frequent ra barmigardanad
        public List<node> Cf(List<node> a)
        {
            //answer jadvale frequente tolid shode khahad bud
            List<node> answer = new List<node>();

            //dar marhaleye aval ke step=1 ast tedade tekrare itemset-haye tak ozvi ra mishomarim
            //va an-haii ke kamtar az minsuppoert ast ra hazf mikonim
            //va jadvale hasel frequent ast
            if (step == 1)
            {
                int counter = 0, j = 0;
                for (int i = 0; i <a.Count ; i++)
                {
                    counter = 0;
                    for (j = 0; j < sourceList.Count; j++)
                    {
                        if (sourceList[j].Contains(a[i].ozv[0]))
                            counter++;
                    }
                    a[i].count = counter;
                    if (counter < minsup)
                    {
                        a.RemoveAt(i);
                        i--;
                    }
                        
                }
                step++;
                return a;
            }
            //agar dar marhaleye dovom be bad bashim varede else mishavim
            //dar in marhale ebteda item set ha ra motenaseb ba marhale join mikonim(khandane tozihate tabe-e createmaj lazem ast)
            //agar hasele joine tak take aza null nabud an ra negah dashte va sepas tedade tekrare an ra dar dataset mishomarim
            // va agar kamtar az min_support bud an ra hazf mikonim
            else
            {
                for (int i = 0; i < a.Count; i++)
                {
                    for (int j = i + 1; j < a.Count; j++)
                    {
                        node n = createmaj(a[i], a[j], step);
                        if (n != null)
                        {
                            bool exist = false;
                            foreach (var VARIABLE in answer)
                            {
                                if (VARIABLE.ToString() == n.ToString())
                                {
                                    exist = true;
                                    break;
                                }
                            }
                            if (exist == false)
                                answer.Add(n);
                        }
                    }
                }

                int counter = 0,k,q;
                for (int i = 0; i < answer.Count; i++)
                {
                    counter = 0;
                    for (k = 0; k < sourceList.Count; k++)
                    {
                        for (q = 0; q < answer[i].ozv.Count; q++)
                        {
                            if (!sourceList[k].Contains(answer[i].ozv[q]))
                                break;
                        }
                        if (answer[i].ozv.Count <= q)
                            counter++;

                    }
                    answer[i].count = counter;
                    if (counter < minsup)
                    {
                        answer.RemoveAt(i);
                        i--;
                    }
                }
                step++;
            }

            //agar liste ma ozvi dashte bashad(answer.cont>0) yani jadvale ferequente ma khali nist
            //agar khali bashad null barmigardanim va dar sharte while-ii ke dar main() ast kare ma tamam mishavad
            //va akharin Activefrequent javabe mast
            if (answer.Count == 0)
                return null;
            return answer;
        }

        //tabe-e createmaj 2 node va 1 tool migirad
        //masalan node {a,b} va node {a,b,c}
        //yek liste khali(temp) dar nazar gereftam
        //azaye 2 node ra agar dar temp nabashand ezafe mikonam be temp
        //hal dar temp kolle azaye gheyre tekrarie 2 node ra darim
        //ke dar in mesal mishavad {a,b,c}
        //agar toole ma 3 bashad va toole liste temp ham ke 3 ast
        //yani joine in 2 majmue yek majmue be toole 3 be ma midahad ke dorost ast 
        // va an ra barmigardanim
        public node createmaj(node a, node b, int lenght)
        {
            List<string> temp=new List<string>();
            foreach (var VARIABLE in a.ozv)
            {
                if(!temp.Contains(VARIABLE))
                    temp.Add(VARIABLE);
            }
            foreach (var VARIABLE in b.ozv)
            {
                if (!temp.Contains(VARIABLE))
                    temp.Add(VARIABLE);
            }
            temp.Sort();
            if (temp.Count == lenght)
            {
                node n = new node();
                n.ozv = temp;
                return n;
            }
            else return null;
        }

        //ba zadane buttone start varede in tabe mishavim
        private void Buttonstart_Click(object sender, RoutedEventArgs e)
        {
            step = 1;
            ActiveFrequent.Clear();
            sourceList.Clear();
            try
            {
                minsup = int.Parse(TextBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("please enter correct number in Min support!");
                return;
            }

            if (path == "")
            {
                MessageBox.Show("please select CSV file");
                return;
            }

            Label.Content = "program is working";
            var reader = new StreamReader(File.OpenRead(path));

            //chon khate avale vurudi TID va dade haye gheyre niazeman ast,yekbar tabe-e readline ra
            //seda mizanam ta khate aval ra bekhanad vali dar jaii save nemikonam
            reader.ReadLine();
            //ta vagti k vurudi line darad,khat be khat an ra mikhanam
            //ebteda fasele ha ra hazf mikonam az an
            //sepas ba ; va , an ra split mikonam va azaye split shode ra dar list temp mirizam
            //chon khaneye avale list TID ast va niaz nadarim be an,an ra hazf mikonam
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                line = line.Replace(" ", "");
                var values = line.Split(';', ',');
                List<string> temp = new List<string>();
                foreach (var value in values)
                {
                    temp.Add(value);
                }
                temp.RemoveAt(0);
                sourceList.Add(temp);
            }
            //liste azaye-majmue shamele tamame a-zaye gheyre tekrarie tarakonesh hast ke an ra be dast avordam
            List<string> azaye_majmue = new List<string>();
            foreach (var item in sourceList)
            {
                foreach (string VARIABLE in item)
                {
                    if (!azaye_majmue.Contains(VARIABLE))
                    {
                        azaye_majmue.Add(VARIABLE);
                    }
                }
            }
            //baraye in k ba-dan betavanam azaye tekrarie maj-mue haii ke dorost mishavad ra hazf konam
            //liste azaye-majmue ra yekbar sort mikonam
            azaye_majmue.Sort();
            //sasakhtane jadvale avalie-ye aza dar list
            //masalan:
            //{a} 0
            //{b} 0
            //{a,b} 0
            //ke be surate default tedade an-ha 0 ast
            foreach (var VARIABLE in azaye_majmue)
            {
                node n = new node();
                n.ozv.Add(VARIABLE);
                ActiveFrequent.Add(n);
            }
            //tabe-e CF ebteda jadvale C ra ijad mikonad va tedade har maj-mue ra dar kole dataset mishomarad
            //sepas jadvale F ra tashkil dade va an-haii k frequente an-ha az maxsupport kamtar bashad hazf mikonad
            //ta jaii k digar natavanad jadvali tolid konad va tabe-e CF null bargardanad
            //agar null bargardanad dar ActiveF akharin jadvali k dorost shode bud vujud darad k haman javab ast
            while (true)
            {
                List<node> frequentlist = Cf(ActiveFrequent);
                if (frequentlist != null)
                    ActiveFrequent = frequentlist;
                else
                {
                    if (step == 1)
                    {
                        ActiveFrequent = null;
                    }
                    break;
                }
                    
            }
            DataGrid.ItemsSource = ActiveFrequent;
            DataGrid.Items.Refresh();
            Label.Content = "Finished!";
        }

        private void MainWindow_OnClosed(object sender, EventArgs e)
        {
            MessageBox.Show("produced by Farshid Sadr in ChangizSoft company!\n© Copyright 2016 ©", "Informatin", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}

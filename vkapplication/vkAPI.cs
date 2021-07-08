using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using VkNet;
using VkNet.Model;
using VkNet.Model.RequestParams;
using Microsoft.VisualBasic;

namespace vkapplication
{

    public partial class vkAPI : Form
    {
        int currentYear = DateTime.Now.Year;
        public vkAPI()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        public static string getAuthForUser()
        {
            string token = "";
            try
            {
                token = File.ReadAllText(@"gaming.txt");
            }
            catch (Exception)
            {
                MessageBox.Show("Message");
            }
            return token;
        }
        private void button1_Click(object sender, EventArgs e)
        {

            var api_user = new VkApi(); 
            try
            {
                api_user.Authorize(new ApiAuthParams
                {
                    AccessToken = getAuthForUser()
                });
            }
            catch (Exception)
            {
                MessageBox.Show("Error!");
            }

            var getFriends = api_user.Friends.Get(new FriendsGetParams
            {
                Fields = VkNet.Enums.Filters.ProfileFields.All

            });


            var ListofAges = new List<int>();
            var ActualAges = new List<int>();
            int averageAge = 0;

            foreach (User user in getFriends)
            {
                listBox1.Items.Add(Encoding.UTF8.GetString(Encoding.GetEncoding(1251).GetBytes(user.FirstName)));
                if (user.BirthDate == null)
                {
                    continue;
                }
                else if (user.BirthDate.Length > 5)
                {
                    int swap = Convert.ToInt32(Strings.Right(user.BirthDate, 4));
                    ListofAges.Add(swap);
                }
            }

            for (int i = 0; i < ListofAges.Count; i++)
            {
                int ye = currentYear - ListofAges[i];
                ActualAges.Add(ye);
                listBox3.Items.Add(ye);
            }


            for (int i = 0; i < ActualAges.Count; i++)
            {
                averageAge += ActualAges[i];
            }

            averageAge = averageAge / ActualAges.Count;

            listBox4.Items.Add(averageAge);

            var ids = api_user.Groups.GetMembers(new GroupsGetMembersParams() 
            { 
                GroupId = textBox1.Text, Fields = VkNet.Enums.Filters.UsersFields.FirstNameAbl
            });
            foreach (User user in ids)
                listBox2.Items.Add(Encoding.UTF8.GetString(Encoding.GetEncoding(1251).GetBytes(user.FirstName)));

            var getPosts = api_user.Wall.Get(new WallGetParams{ }) ;

            foreach (var post in getPosts.WallPosts)
            {
                listBox5.Items.Add(Encoding.UTF8.GetString(Encoding.GetEncoding(1251).GetBytes(post.Text)));
                if (post.Attachments.Count > 0)
                {
                    listBox5.Items.Add("К этому посту прикреплен файл.");
                }
            }

            //var get = api_user.Wall.Get(new WallGetParams());
            //foreach (var wallPosts in get.WallPosts)
            //    listBox2.Items.Add(Encoding.UTF8.GetString(Encoding.Default.GetBytes(wallPosts.Text)));
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void vkAPI_Load(object sender, EventArgs e)
        {

        }

        private void User_Click(object sender, EventArgs e)
        {

        }
    }
        
}



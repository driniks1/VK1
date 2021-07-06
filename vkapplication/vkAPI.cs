﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VkNet;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace vkapplication
{
    public partial class vkAPI : Form
    {
        public vkAPI()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        private string getAuthForGroup()
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Error!");
            }
            return textBox1.Text;
        }
        private string getAuthForUser()
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("Error!");
            }
            return textBox2.Text;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //var api_group = new VkApi();
            //try
            //{
            //    api_group.Authorize(new ApiAuthParams
            //    {
            //        AccessToken = getAuthForGroup()
            //    });
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("Error!");
            //}
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
            var getFriends = api_user.Friends.Get(new VkNet.Model.RequestParams.FriendsGetParams
            {
                Fields = VkNet.Enums.Filters.ProfileFields.All

            });
            foreach (User user in getFriends)
                listBox1.Items.Add(Encoding.UTF8.GetString(Encoding.GetEncoding(1251).GetBytes(user.FirstName)));

            var ids = api_user.Groups.GetMembers(new GroupsGetMembersParams() { GroupId = textBox1.Text, Fields = VkNet.Enums.Filters.UsersFields.FirstNameAbl});
            foreach (User user in ids)
                listBox2.Items.Add(Encoding.UTF8.GetString(Encoding.GetEncoding(1251).GetBytes(user.FirstName)));

            //var get = api_user.Wall.Get(new WallGetParams());
            //foreach (var wallPosts in get.WallPosts)
            //    listBox2.Items.Add(Encoding.UTF8.GetString(Encoding.Default.GetBytes(wallPosts.Text)));
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
        
}



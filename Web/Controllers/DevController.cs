using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MediaCommMvc.Web.Account;
using MediaCommMvc.Web.Forum;
using MediaCommMvc.Web.Forum.Models;
using MediaCommMvc.Web.Forum.ViewModels;
using MediaCommMvc.Web.Models;

namespace MediaCommMvc.Web.Controllers
{
    [Authorize]
    public partial class DevController : RavenController
    {
        private readonly UserStorage userStorage;

        private readonly ForumStorageWriter forumStorageWriter;

        private static Random random = new Random();

        public DevController(UserStorage userStorage, ForumStorageWriter forumStorageWriter)
        {
            this.userStorage = userStorage;
            this.forumStorageWriter = forumStorageWriter;
        }

        public virtual ActionResult FillForum()
        {
            this.CreateUsers();

            this.CreateTopics();

            return this.RedirectToAction(MVC.Forum.Index());
        }

        private void CreateTopics()
        {
            for (int i = 0; i < 500; i++)
            {
                var topic = new EditTopicViewModel
                {
                    Text = GetRandomText(i),
                    Title = GetRandomTopicTitle(i)
                };

                if (i % 5 == 0)
                {
                    topic.Poll = new CreatePollViewModel { Question = GetRandomPollQuestion(), Answers = GetRandomPollAnswers() };
                }

                string topicId = this.forumStorageWriter.SaveTopic(topic, GetRandomUser());

                for (int j = 0; j < i; j++)
                {
                    this.forumStorageWriter.AddReply(new ReplyViewModel { Text = GetRandomText(j), TopicId = topicId }, GetRandomUser());
                }
            }
        }

        private static IList<string> GetRandomPollAnswers()
        {
            var titles = new List<List<string>>
                             {
                               new List<string> { "y", "n" },
                               new List<string> { "I'm in" },
                               new List<string> { DateTime.UtcNow.ToString("F"), DateTime.UtcNow.ToString("F")},
                               new List<string> { DateTime.UtcNow.ToString("F"), DateTime.UtcNow.ToString("F"), DateTime.UtcNow.ToString("F"), DateTime.UtcNow.ToString("F"), DateTime.UtcNow.ToString("F"), DateTime.UtcNow.ToString("F"), DateTime.UtcNow.ToString("F"), DateTime.UtcNow.ToString("F")},
                               new List<string> { DateTime.UtcNow.ToString("F"), DateTime.UtcNow.ToString("F"), DateTime.UtcNow.ToString("F"),"y", "none", DateTime.UtcNow.ToString("F"), DateTime.UtcNow.ToString("F"), DateTime.UtcNow.ToString("F"), DateTime.UtcNow.ToString("F"), DateTime.UtcNow.ToString("F")},
                               new List<string> { DateTime.UtcNow.ToString("F"), DateTime.UtcNow.ToString("F"), DateTime.UtcNow.ToString("F"), DateTime.UtcNow.ToString("F"), DateTime.UtcNow.ToString("F"), DateTime.UtcNow.ToString("F"), DateTime.UtcNow.ToString("F"), DateTime.UtcNow.ToString("F"), DateTime.UtcNow.ToString("F"), DateTime.UtcNow.ToString("F"), DateTime.UtcNow.ToString("F"), DateTime.UtcNow.ToString("F"), DateTime.UtcNow.ToString("F"), DateTime.UtcNow.ToString("F")},
                             };

            int index = random.Next(titles.Count);

            return titles[index];
        }

        private void CreateUsers()
        {
            try
            {
                var user = new User { Email = "u1@loc.loc", UserName = "u1" };
                user.SetPassword("ChangeIt!");
                this.userStorage.CreateUser(user);
            }
            catch (Exception)
            {
                // user may already exist, we don't care here
            }

            try
            {
                var user2 = new User { Email = "justanotheruserwithalongname@loc.loc", UserName = "justanotheruserwithalongname" };
                user2.SetPassword("ChangeIt!");
                this.userStorage.CreateUser(user2);
            }
            catch (Exception)
            {
                // user may already exist, we don't care here
            }

            try
            {
                var user2 = new User { Email = "justanotheruser@loc.loc", UserName = "justanotheruser" };
                user2.SetPassword("ChangeIt!");
                this.userStorage.CreateUser(user2);
            }
            catch (Exception)
            {
                // user may already exist, we don't care here
            }

            try
            {
                var user2 = new User { Email = "userX@loc.loc", UserName = "userX" };
                user2.SetPassword("ChangeIt!");
                this.userStorage.CreateUser(user2);
            }
            catch (Exception)
            {
                // user may already exist, we don't care here
            }
        }

        private static string GetRandomText(int i)
        {
            var titles = new List<string>
                            {
                                "text " + i,
                                "this is just a short text " + i,
                                "this is a normal text with a long word Rindfleischetikettierungsueberwachungsaufgabenuebertragungsgesetz wefrui efruihwefrih efrhuiwefh efrihawefr " + "werifhaweruifh 4 hwiefr hweifhu weuifhwefihu weruifh wefrhui wefuih wefuifhf wer " + i,
                                "a very long text o f nonsense  Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque sed tortor purus. Fusce scelerisque, diam non congue accumsan, dolor nunc consectetur libero, vel posuere risus nulla quis risus. Duis ac enim aliquam, dapibus felis non, elementum ex. Nulla sollicitudin commodo augue, a euismod nibh egestas sit amet. Aliquam fermentum justo at interdum pellentesque. Cras eget turpis non sapien hendrerit efficitur. Integer laoreet eu arcu porta hendrerit. Aenean consectetur lorem in tortor vulputate venenatis. Donec scelerisque hendrerit nisl eu porta. In vestibulum enim id orci egestas vestibulum. Nunc quis odio neque.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer at purus quam. Vivamus non tellus a est convallis suscipit. Pellentesque ac lorem eu dolor sagittis hendrerit. Ut feugiat mauris metus, vel ultricies metus sodales eu. Cras pretium mauris ex, quis finibus sem fringilla at. Ut et velit id massa laoreet maximus id ac odio. Pellentesque iaculis risus in porta elementum. Etiam condimentum lobortis metus vel dictum. Nulla pharetra euismod lorem, ac varius nibh hendrerit et. Proin tincidunt faucibus euismod.Ut et aliquet mi. Maecenas convallis metus nec mi fringilla, quis tincidunt nisi eleifend. Donec consequat elit id volutpat egestas. Proin pellentesque ligula lectus, ac imperdiet diam sodales vel. Nam ac velit eget nulla posuere dictum sed ac ex. Quisque tincidunt auctor venenatis. Phasellus id felis non eros dictum imperdiet vel eget magna.Aliquam malesuada finibus sapien, at sagittis lorem malesuada et. Duis at rhoncus ex. Etiam iaculis nunc neque. Aliquam rhoncus eros eu lorem suscipit, et ullamcorper ex condimentum. Morbi molestie ante eget erat imperdiet commodo. Aliquam ac turpis a libero ultricies pharetra. Sed in sapien ullamcorper, luctus nisi ac, maximus dolor. Nam sed ante mattis, vehicula lacus ac, gravida nisl. Morbi gravida aliquam sapien a semper. Etiam eget imperdiet magna, et sollicitudin ex. Mauris a pretium lectus, et congue lorem.Donec pulvinar faucibus condimentum. Suspendisse at cursus felis. Pellentesque at condimentum augue. Etiam malesuada ut ligula et fermentum. Donec pretium sem nec ante aliquam pharetra. Etiam sapien justo, varius et fermentum ac, eleifend vitae ante. Duis mattis varius dapibus. Duis consectetur fermentum urna non ultricies. Curabitur efficitur neque eget enim aliquam, ac suscipit erat tempus. Praesent efficitur metus nec velit dignissim, eget mattis leo semper. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae;Nulla pulvinar vitae augue nec interdum. Etiam interdum eros arcu, eu lobortis magna gravida ullamcorper. Pellentesque sit amet elit aliquet, lobortis lectus in, tincidunt leo.Nunc fermentum, velit a porta sodales, mauris enim maximus elit, ac dapibus nulla neque ac purus.Cras eget mauris convallis nibh viverra tincidunt id id orci. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.Aliquam in tincidunt arcu, pellentesque ornare massa.Nunc orci nisi, blandit id nisi ut, blandit sagittis dolor. Ut eleifend sem at leo maximus mattis.Vivamus sodales finibus diam quis malesuada. Sed elementum nisl ut leo tincidunt interdum.Nulla ullamcorper at lorem in ornare.Aliquam malesuada nibh at fermentum ultricies. Proin volutpat erat arcu, et aliquet metus hendrerit." + i
                            };

            int index = random.Next(titles.Count);

            return titles[index];
        }

        private static string GetRandomPollQuestion()
        {
            var titles = new List<string>
                             {
                                 "? ",
                                 "Do you want to join?",
                                 "How would you answer this question, if it was asked at a random time by a random user? ",
                                 "The length of this question is totally absurd wefuiohwefr wefrihuwegfr  werhjwefr wegfr wegriohwegjhuioerjuiogrjiogrjiogr griohjgeriojegr  jioj iouhj hjui uioegregruopoeru8g  juoijuiojuegr jkipoüui iou j iloj griojioj  jioergj ioaegr jegr mjio egrgjiop ae egrjiop r"
                             };

            int index = random.Next(titles.Count);

            return titles[index];
        }

        private static string GetRandomTopicTitle(int i)
        {
            var titles = new List<string>
                            {
                                "t" + i,
                                "new topic title " + i,
                                "this is a normal title " + i,
                                "a very long title which will could be a little bit too long for a small smartphone display like Nexus 4 or a similar device " + i
                            };

            int index = random.Next(titles.Count);

            return titles[index];
        }

        private static string GetRandomUser()
        {
            var users = new List<string>
                            {
                                "u1",
                                "userX",
                                "justanotheruser",
                                "justanotheruserwithalongname",
                                "Admin"
                            };

            int index = random.Next(users.Count);

            return users[index];
        }
    }
}
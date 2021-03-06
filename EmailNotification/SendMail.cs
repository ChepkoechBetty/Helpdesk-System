using EAGetMail;
using Helpdesk.Data;
using Helpdesk.Models;
using Helpdesk.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailNotification
{
    class SendMail
    {
        List<string> UserId=new List<string>();
        List<int> ids =new List<int>();
        int ClientId;
        List<string> Emails=new List<string>();
        protected HelpdeskContext db = new HelpdeskContext();
        static readonly string connString = "Data Source=DESKTOP-AKKIQ4U;Initial Catalog=Helpdesk;Persist Security Info=True;User ID=sa;Password=chep@610;MultipleActiveResultSets=True";
        SqlConnection conn = new SqlConnection(connString);
        private int OpenConnection()
        {
            var response = 0;
            try
            {
                conn.Open();
                LogData("Database Opened Successfully.");
                response = 1;
            }
            catch (Exception e)
            {
                LogData("Error Opening Database >> " + e.Message);
                LogData("Service will EXIT...");

            }
            return response;
        }
        public void Email()
        {
            if (OpenConnection() == 0) { return; }
            LogData("Start Sync");
            MailServer oServer = new MailServer("pop.xeran.com", "support@spasys.com", "Kingori123;", ServerProtocol.Imap4);
            MailClient oClient = new MailClient("TryIt");

            oServer.SSLConnection = true;
            oServer.Port = 993;

            //oServer.SSLConnection = false;
            //oServer.Port = 143;

            oClient.GetMailInfosParam.GetMailInfosOptions = GetMailInfosOptionType.NewOnly;
            try
            {
                oClient.Connect(oServer);
                MailInfo[] infos = oClient.GetMailInfos();

                for (int i = infos.Length - 1; i > 0; i--)
                {
                    MailInfo info = infos[i];
                    Mail oMail = oClient.GetMail(info);
                    var count = oMail.Attachments.ToList().Count;
                    for (int j = 0; j < count; j++)
                    {
                        //oMail.Attachments[j].SaveAs(Server.MapPath("~/Inbox") + "\\" + oMail.Attachments[j].Name, true); // true for overWrite file
                    }
                    SqlCommand cd;
                    SqlDataReader dRder;
                    string qy;
                    string[] c = oMail.From.Address.Split('@');
                    string[] clientname = c[1].Split('.');
                    string name = clientname[0];
                    SqlCommand sc;
                    SqlDataReader sd;
                    string s;
                    s = "Select * From clients where REPLACE(Name, ' ', '')=" + "'" + name + "'";
                    sc = new SqlCommand(s, conn);
                    sd = sc.ExecuteReader();
                    while (sd.Read())
                    {
                        ClientId = Convert.ToInt32(sd["Id"]);

                    }
                    sd.Close();
                    qy = "Insert into tickets(userid,clientid,branchid,tickettypeid,subject,emaillist,details,ticketpriorityid,productid,ticketstatus,datereceived,departmentid) values('9cbc56bd-7426-450d-8df2-fccd753ce56a'," + ClientId + ",0,1,'" + oMail.Subject + "',NULL,'" + oMail.TextBody + "',0,11,1,'" + oMail.SentDate + "',0)";
                    cd = new SqlCommand(qy, conn);
                    cd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                LogData("error reading emails" + e.Message);
            }
            try
            {
                SqlCommand command;
            SqlDataReader dataReader;
            string sql;
                var today = DateTime.Now;
            sql = "Select * From TicketAllocations where completiondate<="+"'"+today+"'"+"and alerted=0";
            command = new SqlCommand(sql, conn);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                    int id =Convert.ToInt32(dataReader["Id"]);
                    var userid = dataReader["UserId"].ToString();
                    ids.Add(id);
                    UserId.Add(userid);
                    //for (int i = 0; i <= dataReader.FieldCount - 1; i++)
                    //{
                    //    UserId.Add(dataReader[i].ToString());
                    //}
            }
                dataReader.Close();
                
                foreach(int i in ids)
                {
                    SqlCommand cd;
                    SqlDataReader dRder;
                    string qy;
                    qy = "Update TicketAllocations set alerted=1 where Id="+ i;
                    cd = new SqlCommand(qy, conn);
                    cd.ExecuteNonQuery();
                }

                foreach (var user in UserId)
                {
                    SqlCommand cmd;
                    SqlDataReader dReader;
                    string qry;
                    qry= "Select Email From Aspnetusers where Id="+"'"+user+"'";
                    cmd = new SqlCommand(qry, conn);
                    dReader = cmd.ExecuteReader();
                    while (dReader.Read())
                    {
                        for (int i = 0; i <= dReader.FieldCount - 1; i++)
                        {
                            Emails.Add(dReader[i].ToString());
                        }
                        //Emails.Add(dReader.GetValue(0).ToString());
                    }
                    dReader.Close();
                    foreach (var email in Emails)
                    {
                        User usr = new User();
                        NetworkCredential cred = new NetworkCredential("spasysalerts@gmail.com", "$p@sysAlerts");
                        MailMessage msg = new MailMessage();
                        msg.To.Add(email);
                        msg.Subject = "Due Ticket";
                        msg.Body = "The ticket you were assigned is due.Login to Spasys Helpdesk and check.";
                        msg.From = new System.Net.Mail.MailAddress("email@apsissolutions.com"); // Your Email Id
                        SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                        SmtpClient client1 = new SmtpClient("smtp.mail.yahoo.com", 465);
                        client.UseDefaultCredentials = true;
                        client.Credentials = cred;
                        client.EnableSsl = true;
                        client.Send(msg);
                        //var emailSubj = "Ticket Processed and Assigned.";
                        //var body = EmailAlertService.AlertTicketOwnerOnAssignment("Your assigned ticket is due", usr);
                        //var from = "spasyshelpdesk@donotreply.com";
                        //var to = email;
                        //UserService.SendEmail(from, to, emailSubj, body, "");

                    }

                }

                //DateTime today = DateTime.Now;
                //var Tuser = db.TicketAllocations.Where(x => x.CompletionDate.ToString() == "2020 - 02 - 05 14:36:03.000").ToList();
                //if (Tuser != null)
                //{
                //    foreach (var u in Tuser)
                //    {
                //        var users = db.Users.Where(x => x.Id == u.UserId).ToList();
                //        foreach (var user in users)
                //        {
                //var usermail=user.Email
                // var emailSubj = "Ticket Processed and Assigned.";
                // var body = EmailAlertService.AlertTicketOwnerOnAssignment("Your assigned ticket is due", user);
                // var from = "spasyshelpdesk@donotreply.com";
                // var to = user.Email;
                //UserService.SendEmail(from, to, emailSubj, body, "");
                //        }
                //    }
                //}
                LogData("End Sync");
            }
            catch(Exception e)
            {
                LogData("Error sending mails: " + e.Message);
            }
        }
        public void LogData(string data)
        {
            string pflder = AppDomain.CurrentDomain.BaseDirectory + "Logs";
            if (!Directory.Exists(pflder))
            {
                Directory.CreateDirectory(pflder);
            }
            string dt = DateTime.Now.ToString("ddMMyyyy");
            string path = pflder + "\\SystemLog_" + dt + ".log";
            data = DateTime.Now.ToString("dd-MM-yyyy hh:mm ss") + " : " + data;
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
                using (TextWriter tw = new StreamWriter(path))
                {
                    tw.WriteLine(data);
                    tw.Close();
                }
            }
            else if (File.Exists(path))
            {
                try
                {
                    StreamWriter swAppend = File.AppendText(path);
                    swAppend.WriteLine(data);
                    swAppend.Close();

                }
                catch (Exception)
                {
                }

            }
        }

    }
}

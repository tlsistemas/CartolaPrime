using CartolaPrime.SendPush.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace CartolaPrime.SendPush
{
    class Program
    {
        static void Main(string[] args)
        {
            var item = new PushModel
            {
                FCMToken = "ett_GlVoTTOUa2UyCxApIL:APA91bFyba-TL6wYMnYcHegUgbRbHqwFfgTxjDziAoVQFDfbBIbcg2mBKgb4-b4myi7s_pXEaLMY5dhpX3lopaR9byLwWkxOejjlx5H7rD9LPk02ESWCKQv3mC0vOkNvH7e1Ybn9o2-9",
                Title = "Gol do Vasco",
                Message = "E mentira abestardo",
                Version = "1.0",
                Android = true,
                Push = 0,
                Link = ""
            };

            StepSendPush(item);
        }

        public static void StepSendPush(PushModel item)
        {
            dynamic ent;
            switch ((bool)item.Android)
            {
                case true:
                    switch ((string)item.Version)
                    {
                        case "1.0":
                            ent = new
                            {
                                validate_only = false,
                                message = new
                                {
                                    token = item.FCMToken, //"crn0BlOLSnKUegDiCLbPd1:APA91bE10R9_ZkL_cG5uRjCiQVbfI-8CL2bJh0QidUdz07O2g5R_Ck6lamdfG-YoLVv4qT3sNITBbPfr1JevqKryOQ-mJa2bMxz8JI7UTTgKKHdgzkDIzxrzx_srSZMH3BAI-vJoRFd6",
                                    data = new
                                    {
                                        title = item.Title,
                                        body = item.Message,
                                        content = JsonConvert.SerializeObject(new
                                        {
                                            link = "https://clubedepremios.com.br" + item.Link,
                                            icon = item.Icon != null ? "https://clubedepremios.com.br/assets/img/push/icon/" + item.Icon : null,
                                            image = item.Image != null ? "https://clubedepremios.com.br/assets/img/push/" + item.Image : null,
                                            content = item.Message,
                                            categoryID = item.CodeType,
                                            action1 = JsonConvert.SerializeObject(new
                                            {
                                                text = "ABRIR",
                                                link = "https://clubedepremios.com.br" + item.Link
                                            }),

                                            action2 = JsonConvert.SerializeObject(new
                                            {
                                                text = "MARCAR COMO LIDO"
                                            })
                                        })
                                    }
                                }
                            };
                            break;
                        default:
                            ent = new
                            {
                                validate_only = false,
                                message = new
                                {
                                    token = item.FCMToken,
                                    data = new
                                    {
                                        WX_PUSH_EXT_VERSION = "1.0",
                                        WX_PROP_ACTIVEAPPLICATION = "false",
                                        WX_PROP_CONTENU = item.Push.ToString()
                                    }
                                }
                            };
                            break;
                    }
                    break;
                case false:
                    ent = new
                    {
                        message = new
                        {
                            token = item.FCMToken,
                            notification = new
                            {
                                title = item.Title,
                                body = item.Message,
                            },
                            apns = new
                            {
                                payload = new
                                {
                                    action = item.Link,
                                    titulo = item.Title,
                                    corpo = item.Message,
                                    aps = new Dictionary<string, object>
                                    {
                                        { "badge", 0 },
                                        { "content-available", 1 }
                                    }
                                }
                            }
                        }
                    };
                    break;
            }

            var token = cFCM.GetTokenAndCall();

            var wc = new WebClient();
            string accessTokenFromJSONKey = token;
            wc.Headers.Add("User-Agent", "google-api-dotnet-client/1.38.2.0 (gzip)");
            wc.Headers.Add("Authorization", "Bearer " + accessTokenFromJSONKey);
            wc.Headers.Add("Content-Type", "application/json; charset=utf-8");
            var bytes = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(ent));
            try
            {
                bytes = wc.UploadData("https://fcm.googleapis.com/v1/projects/cartoprime-5990c/messages:send", bytes);
                var s = System.Text.Encoding.UTF8.GetString(bytes);
            }
            catch (WebException ex)
            {
                using (var sr = new StreamReader(ex.Response.GetResponseStream()))
                {
                    string ret = sr.ReadToEnd();
                }
            }
        }
    }
}

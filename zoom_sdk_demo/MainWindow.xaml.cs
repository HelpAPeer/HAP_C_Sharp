using System;
using System.Collections.Generic;
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
using System.ComponentModel; // CancelEventArgs
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using ZOOM_SDK_DOTNET_WRAP;

namespace zoom_sdk_demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        start_join_meeting start_meeting_wnd = new start_join_meeting();
        IAccountInfo account = null;

        public MainWindow()
        {
            InitializeComponent(); 
        }

        //callback
        public void onAuthenticationReturn(AuthResult ret)
        {
            if (ZOOM_SDK_DOTNET_WRAP.AuthResult.AUTHRET_SUCCESS == ret)
            {
                Console.WriteLine("Auth Success");
                account = ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetAuthServiceWrap().GetAccountInfo();
                if (account == null)
                {
                    tryLogin();
                }
                else
                {
                    Console.WriteLine(account.GetDisplayName());
                    Console.WriteLine(account.GetLoginType());
                    if(account.GetDisplayName().Length == 0 && account.GetLoginType() == ZOOM_SDK_DOTNET_WRAP.LoginType.LoginType_Unknown)
                    {
                        tryLogin();
                    }
                    else
                    {
                        start_meeting_wnd.Show();
                    }
                    
                }
            }
            else//error handle.todo
            {
                Console.WriteLine("Auth Failed");
                Console.WriteLine(ret);
                Show();
            }
        }
        public void onLoginRet(LOGINSTATUS ret, IAccountInfo pAccountInfo)
        {
            Console.WriteLine("Login Returned");
            Console.WriteLine(ret.ToString());
            if (ZOOM_SDK_DOTNET_WRAP.LOGINSTATUS.LOGIN_SUCCESS == ret)
            {
                account = pAccountInfo;
                Console.WriteLine(account.GetDisplayName());
                start_meeting_wnd.Show();
                account = pAccountInfo;
            }
            //todo
        }
        public void onLogout()
        {
            Console.WriteLine("Logged out");
            Console.WriteLine(ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetAuthServiceWrap().GetAccountInfo().GetDisplayName());
        }
        private void button_auth_Click(object sender, RoutedEventArgs e)
        {
            //register callback
            Console.WriteLine("Hello World");
            ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetAuthServiceWrap().Add_CB_onAuthenticationReturn(onAuthenticationReturn);
            ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetAuthServiceWrap().Add_CB_onLoginRet(onLoginRet);
            ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetAuthServiceWrap().Add_CB_onLogout(onLogout);
            //
            ZOOM_SDK_DOTNET_WRAP.AuthContext param = new ZOOM_SDK_DOTNET_WRAP.AuthContext();
            

            string genToken = generateJWT();
            Console.WriteLine(genToken);
            param.jwt_token = genToken; 

            
            ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetAuthServiceWrap().SDKAuth(param);
            Hide();
        }

        private string generateJWT()
        {
            Console.WriteLine("Generating token");
            //TODO: Find more secure means of storing key secret
            const string secret = "7yJxBb46Rc2hDGv7MJKQbnRFTpQJUWYd5RBI";
            const string key = "xneAyeSlgvlxmJYnEuBqMx4wLIKRki7381ma";

            // Generating the time values for token
            var now = DateTimeOffset.UtcNow.AddMilliseconds(-30);
            var epoch = new DateTimeOffset(1970, 1, 1, 0, 1, 0, TimeSpan.Zero);
            var iat = (long) (now - epoch).TotalSeconds;
            var exp = (long)(now.AddHours(40) - epoch).TotalSeconds;
            var tokenExp = (long)(now.AddMinutes(45) - epoch).TotalSeconds;

            Console.WriteLine("Generating secret");
            var sdkkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            Console.WriteLine("Generating Claims");
            var claims = new[]
            {
                new Claim("appKey",key),
                new Claim(JwtRegisteredClaimNames.Iat, iat.ToString() , ClaimValueTypes.Integer64),
                new Claim(JwtRegisteredClaimNames.Exp, exp.ToString(), ClaimValueTypes.Integer64),
                new Claim("tokenExp", tokenExp.ToString(), ClaimValueTypes.Integer64)
            };

            Console.WriteLine("Generating components");
            var payload = new JwtPayload(claims);
            var sign = new SigningCredentials(sdkkey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(sign);

            Console.WriteLine("Generating token");
            var token = new JwtSecurityToken(header, payload);
            var handler = new JwtSecurityTokenHandler();

            Console.WriteLine("Finished generating token");
            return handler.WriteToken(token);
        }


        public void tryLogin()
        {
            ZOOM_SDK_DOTNET_WRAP.LoginParam param = new ZOOM_SDK_DOTNET_WRAP.LoginParam();
            ZOOM_SDK_DOTNET_WRAP.LoginParam4Email cred = new ZOOM_SDK_DOTNET_WRAP.LoginParam4Email();

            string username = textBox_username.Text;
            string password = textBox_password.Password;

            cred.userName = username;
            cred.password = password;
            cred.bRememberMe = false;

            param.emailLogin = cred;
            param.loginType = ZOOM_SDK_DOTNET_WRAP.LoginType.LoginType_Email;

            Console.WriteLine("Sending out login message");
            ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetAuthServiceWrap().Login(param);
        }

        void Wnd_Closing(object sender, CancelEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}

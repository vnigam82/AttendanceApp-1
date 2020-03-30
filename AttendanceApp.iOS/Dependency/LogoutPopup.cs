using System;
using System.Threading.Tasks;
using AttendanceApp.Views;
using CoreGraphics;
using UIKit;

namespace AttendanceApp.iOS.Dependency
{
    public class LogoutPopup:UIView
    {
        public delegate void PopWillCloseHandler();
        public event PopWillCloseHandler PopWillClose;


        private UIVisualEffectView effectView = new UIVisualEffectView(UIBlurEffect.FromStyle(UIBlurEffectStyle.Dark));
        private UIButton btnYes = new UIButton(UIButtonType.System);
        private UIButton btnNo = new UIButton(UIButtonType.System);
        public LogoutPopup(CGSize size)
        {
            nfloat lx = (UIScreen.MainScreen.Bounds.Width - size.Width) / 2;
            nfloat ly = (UIScreen.MainScreen.Bounds.Height - size.Height) / 2;
            this.Frame = new CGRect(new CGPoint(lx, ly), size);

            effectView.Alpha = 0;

            this.BackgroundColor = UIColor.White;


            UIView mainView = new UIView();
            mainView.BackgroundColor = UIColor.White;
            UIView headerView = new UIView();
            UIView belowHeaderView = new UIView();
            belowHeaderView.BackgroundColor = UIColor.FromRGB(244, 126, 37);
            UILabel headerlabel = new UILabel();
            UILabel logoutmessagetext = new UILabel();
            var logoutmessagetextFrame = logoutmessagetext.Frame;


            var mainviewframe = mainView.Frame;
            var headerviewframe = headerView.Frame;
            var headerlabelFrame = headerlabel.Frame;

            headerView.BackgroundColor = UIColor.FromRGB(244, 126, 37);
            //belowHeaderView.BackgroundColor = UIColor.FromRGB(31, 73, 125);

            headerlabel.Text = "Log out";
            headerlabel.TextColor = UIColor.White;
            headerlabel.Font = UIFont.FromName("Helvetica-Bold", 16f);
            headerlabelFrame = new CGRect(10, 10, this.Frame.Width, 30);
            headerlabel.Frame = headerlabelFrame;

            headerView.AddSubview(headerlabel);
            headerviewframe = new CGRect(1, 1, this.Frame.Width - 2, headerlabelFrame.Height + 20);
            headerView.Frame = headerviewframe;
            mainView.AddSubview(headerView);


            logoutmessagetext.Text = "Are you sure to log out from Application?";
            logoutmessagetext.TextColor = UIColor.White;
            logoutmessagetext.Lines = 0;
            logoutmessagetext.LineBreakMode = UILineBreakMode.WordWrap;
            logoutmessagetext.TextAlignment = UITextAlignment.Center;
            logoutmessagetextFrame.Height = 50;
            logoutmessagetextFrame.Y = 30;
            logoutmessagetextFrame.Width = headerviewframe.Width;
            logoutmessagetext.Frame = logoutmessagetextFrame;
            belowHeaderView.AddSubview(logoutmessagetext);

            belowHeaderView.Frame = new CGRect(headerviewframe.X,
                                               headerviewframe.Y + headerviewframe.Height + 1,
                                               this.Frame.Width - 2,
                                               this.Frame.Height);

            nfloat btnHeight = 40;
            btnYes.SetTitle("Yes", UIControlState.Normal);
            btnYes.SetTitleColor(UIColor.White, UIControlState.Normal);
            btnYes.Layer.BorderWidth = 1;
            btnYes.Font = UIFont.FromName("Helvetica-Bold", 16f);
            btnYes.Layer.BorderColor = UIColor.White.CGColor;
            btnYes.BackgroundColor = UIColor.FromRGB(244, 126, 37);

            btnYes.Frame = new CGRect(11, logoutmessagetextFrame.Y + logoutmessagetextFrame.Height + 48, ((belowHeaderView.Frame.Width + 15) - (belowHeaderView.Frame.Width / 2)), btnHeight);
            btnYes.TouchUpInside += delegate {
                Close();
                Task.Run(() => App.Database.ClearLoginDetails());
                if (App.Current.Properties.ContainsKey("UserId"))
                    App.Current.Properties.Remove("UserId");

                if (App.Current.Properties.ContainsKey("IsLoggedIn"))
                    App.Current.Properties.Remove("IsLoggedIn");

                Task.Run(async () => await App.Current.SavePropertiesAsync());
                App.Current.MainPage = new Login();
            };

            belowHeaderView.AddSubview(btnYes);


            btnNo.SetTitle("No", UIControlState.Normal);
            btnNo.SetTitleColor(UIColor.White, UIControlState.Normal);
            btnNo.Layer.BorderWidth = 1;
            btnNo.Font = UIFont.FromName("Helvetica-Bold", 16f);
            btnNo.Layer.BorderColor = UIColor.White.CGColor;
            btnNo.BackgroundColor = UIColor.FromRGB(244, 126, 37);

            btnNo.Frame = new CGRect(btnYes.Frame.X + btnYes.Frame.Width + 8, logoutmessagetextFrame.Y + logoutmessagetextFrame.Height + 48, ((belowHeaderView.Frame.Width - 45) - (belowHeaderView.Frame.Width / 2)), btnHeight);
            btnNo.TouchUpInside += delegate {
                Close();

            };

            belowHeaderView.AddSubview(btnNo);

            belowHeaderView.Frame = new CGRect(headerviewframe.X,
                                               headerviewframe.Y + headerviewframe.Height + 1,
                                               this.Frame.Width - 2,
                                               this.Frame.Height - 2 - (headerView.Frame.Y + headerView.Frame.Height));

            mainView.AddSubview(belowHeaderView);


            mainviewframe = new CGRect(0, 0, this.Frame.Width, this.Frame.Height);
            mainView.Frame = mainviewframe;


            this.AddSubview(mainView);
        }

        public void PopUp(bool animated = true, Action popAnimationFinish = null)
        {
            UIWindow window = UIApplication.SharedApplication.KeyWindow;
            effectView.Frame = window.Bounds;
            window.EndEditing(true);
            window.AddSubview(effectView);
            window.AddSubview(this);

            if (animated)
            {
                Transform = CGAffineTransform.MakeScale(0.1f, 0.1f);
                UIView.Animate(0.15, delegate {
                    Transform = CGAffineTransform.MakeScale(1, 1);
                    effectView.Alpha = 0.8f;
                }, delegate {
                    if (null != popAnimationFinish)
                        popAnimationFinish();
                });
            }
            else
            {
                effectView.Alpha = 0.8f;
            }
        }

        public void Close(bool animated = true)
        {
            if (animated)
            {
                UIView.Animate(0.15, delegate {
                    Transform = CGAffineTransform.MakeScale(0.1f, 0.1f);
                    effectView.Alpha = 0;
                }, delegate {
                    this.RemoveFromSuperview();
                    effectView.RemoveFromSuperview();
                    if (null != PopWillClose) PopWillClose();
                });
            }
            else
            {
                if (null != PopWillClose) PopWillClose();
            }
        }
    }
}

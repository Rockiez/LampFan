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

using ICS.LampAndFan;

using ICS.Acquisition;
using ICS.Common;
using ICS.Models;

namespace LampFan
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    /// 

    public class onofflamp
    {
        public bool lampstate = false;
        public ADAM4150FuncID oncode, offcode;
        public Image lampimg;
        public Image buttonimg;
        public string errormess;

        public void onoffmain()
        {
            var state = Global.ADAM4150Provider.CheckSerialPort(Global.ADAM4150Provider.ADAM4017Provider);
            if (state.Status == RunStatus.Failure)
            {
                MessageBox.Show(state.ResultMessage);
            }
            else
            {
                if (Global.ADAM4150Provider.OnOff(lampstate ? offcode : oncode))
                {
                    lampstate = ! lampstate;
                    resetimg();
                }
                else
                {
                    MessageBox.Show(errormess);
                }
            }
        }


        void resetimg()
        {
            if (lampstate)
            {
                lampimg.Source = new BitmapImage(new Uri("Resources/lamp_on.png", UriKind.Relative));
                buttonimg.Source = new BitmapImage(new Uri("Resources/btn_switch_on.png", UriKind.Relative)); 
            }
            else
            {
                lampimg.Source = new BitmapImage(new Uri("Resources/lamp_off.png", UriKind.Relative));
                buttonimg.Source = new BitmapImage(new Uri("Resources/btn_switch_off.png", UriKind.Relative)); 
            }
        }
    }

    public partial class MainWindow : Window
    {
        onofflamp controlstreet;
        onofflamp controlcorride;

        public MainWindow()
        {
            InitializeComponent();
            controlcorride = new onofflamp { oncode = ADAM4150FuncID.OnCorridorLamp, offcode = ADAM4150FuncID.OffCorridorLamp, lampimg = imgc, buttonimg = imgcc, errormess = "操作楼道灯失败" };
            controlstreet = new onofflamp { oncode = ADAM4150FuncID.OnStreetLamp, offcode = ADAM4150FuncID.OffStreetLamp, lampimg = imgs, buttonimg = imgsc, errormess = "操作街道灯失败" };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
         WindowState = WindowState.Minimized;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
        Close();
        }

        ResultEntity state = Global.ZigBeeProvider.CheckSerialPort(Global.ZigBeeProvider);

        void fancontrol(string errormess, ZigBeeFuncID id)
        {
            if (state.Status == RunStatus.Failure)
            {
                MessageBox.Show(state.ResultMessage);
            }
            else
            {
                if (! Global.ZigBeeProvider.GetSet(id))
                {
                    MessageBox.Show(errormess);
                }
            }
        }
        private void onbutton1(object sender, RoutedEventArgs e)
        {
            fancontrol("操作风扇1失败", ZigBeeFuncID.OnFan1);
        }

        private void offbutton1(object sender, RoutedEventArgs e)
        {
            fancontrol("操作风扇1失败", ZigBeeFuncID.OffFan1);
        }

        private void onbutton2(object sender, RoutedEventArgs e)
        {
            fancontrol("操作风扇2失败", ZigBeeFuncID.OnFan2);
        }

        private void offbutton2(object sender, RoutedEventArgs e)
        {
            fancontrol("操作风扇2失败", ZigBeeFuncID.OffFan2);
        }
        private void Imgs_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
        controlstreet.onoffmain();
        }

        private void Imgsc_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
        controlstreet.onoffmain();
        }

        private void Imgc_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
        controlcorride.onoffmain();
        }

        private void Imgcc_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
        controlcorride.onoffmain();
        }
    }
}

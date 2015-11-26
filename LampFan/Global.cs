using ICS.Acquisition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICS.LampAndFan
{
    public class Global
    {

        public static ZigBee ZigBeeProvider
        {
            get
            {
                return (ZigBee)ClassFactory.GetProvider(equipmentCategory.ZigBee, Global.ComSetting);
            }
        }

        public static ADAM4150 ADAM4150Provider
        {
            get
            {
                return (ADAM4150)ClassFactory.GetProvider(equipmentCategory.ADAM4150, Global.ComSetting);
            }
        }

        public static ICS.Models.Com.ComSettingModel _ComSetting = null;

        public static ICS.Models.Com.ComSettingModel ComSetting
        {
            get
            {
                if (_ComSetting == null)
                {
                    _ComSetting = new ICS.Common.SettingHelper<ICS.Models.Com.ComSettingModel>().GetSetting();
                    _ComSetting.ZigbeeCom = "Com3";
                    _ComSetting.AnalogQuantityCom = "Com2";
                    _ComSetting.DigitalQuantityCom = "Com2";
              
                }

                return _ComSetting;
            }
        }
    }
}
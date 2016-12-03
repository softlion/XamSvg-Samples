﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace XamSvg.UwDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly string[] fileNames;
        private int i;

        public MainPage()
        {
            //Initialize the SVG lib
            #region Move this in App.xaml.cs
            var assembly = typeof(XamSvgDemo.Shared.App).GetTypeInfo().Assembly;
            XamSvg.Shared.Config.ResourceAssembly = assembly;
            #endregion

            this.InitializeComponent();

            var sharedSvgs = assembly.GetManifestResourceNames().Where(n => n.EndsWith(".svg")).OrderBy(n => n.Substring(n.LastIndexOf('/') + 1)).ToArray();
            fileNames = sharedSvgs.Select(s => "res:" + s).ToArray();
        }

        /// <summary>
        /// Change svg on button click
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (fileNames == null)
                return;

            //MySvg.Source = new Uri("ms-appx:///Assets/svg/" + fileNames[i++%fileNames.Length]);
            MySvg.Source = new Uri(fileNames[i++ % fileNames.Length], UriKind.Absolute);

            #region Test load svg from string
            //            var testStringSvg = @"
            //<svg viewBox=""0 0 16.002 16.002"">
            // <path fill=""#ff0002"" d=""M8,0c-4.418,0-8,3.584-8,8,0,4.42,3.582,8.002,8,8.002,4.42,0,8.001-3.582,8.001-8.002,0-4.416-3.581-8-8.001-8zm4.99,10.677-2.315,2.313-2.674-2.676-2.675,2.676-2.314-2.313,2.676-2.678-2.676-2.674,2.314-2.313,2.675,2.674,2.674-2.674,2.315,2.313-2.676,2.674,2.676,2.678z""/>
            // <g fill=""#000"">
            //  <path d=""m4.0446,3.962,0.35742-0.03125c0.016927,0.14323,0.056315,0.26074,0.11816,0.35254,0.061848,0.091797,0.15788,0.16602,0.28809,0.22266,0.13021,0.056641,0.27669,0.084961,0.43945,0.084961,0.14453,3E-7,0.27213-0.021484,0.38281-0.064453,0.1106-0.043,0.1929-0.1019,0.247-0.1768,0.054-0.0748,0.081-0.1565,0.081-0.2451,0-0.0898-0.026-0.1683-0.0781-0.2353-0.0521-0.0671-0.138-0.1234-0.2578-0.169-0.0768-0.0299-0.2467-0.0765-0.5098-0.1396-0.263-0.0632-0.4472-0.1227-0.5527-0.1787-0.1367-0.0716-0.2386-0.1605-0.3057-0.2666-0.067-0.1062-0.1005-0.225-0.1005-0.3565,0-0.1445,0.041-0.2796,0.123-0.4053,0.082-0.1256,0.2018-0.221,0.3594-0.2861,0.15755-0.065101,0.33268-0.097653,0.52539-0.097656,0.21224,0.0000029,0.39941,0.034183,0.56152,0.10254,0.16211,0.068362,0.28678,0.16895,0.37402,0.30176,0.087237,0.13281,0.13411,0.28321,0.14062,0.45117l-0.3632,0.0274c-0.0196-0.181-0.0857-0.3177-0.1983-0.4102-0.1126-0.0924-0.279-0.1387-0.499-0.1387-0.2292,0-0.3962,0.042-0.501,0.126s-0.1572,0.1852-0.1572,0.3037c-7E-7,0.10287,0.037109,0.1875,0.11133,0.25391,0.072916,0.066408,0.26335,0.13444,0.57129,0.2041,0.30794,0.069663,0.5192,0.13054,0.63379,0.18262,0.16666,0.076824,0.28971,0.17415,0.36914,0.29199,0.079425,0.11784,0.11914,0.25358,0.11914,0.40723-0.0000025,0.15234-0.043622,0.2959-0.13086,0.43066-0.0871,0.1348-0.2125,0.2396-0.3759,0.3145s-0.3473,0.1123-0.5517,0.1123c-0.2591,0-0.4763-0.0378-0.6514-0.1133s-0.3125-0.1891-0.4121-0.3408-0.152-0.3232-0.1572-0.5147z""/>
            //  <path d=""m7.6637,4.882-1.1094-2.8633,0.41016,0,0.74414,2.0801c0.059895,0.16667,0.11002,0.32292,0.15039,0.46875,0.044269-0.15625,0.095702-0.3125,0.1543-0.46875l0.77344-2.0801h0.38672l-1.1211,2.8633z"" />
            //  <path d=""m10.857,3.7589,0-0.33594,1.2129-0.00195,0,1.0625c-0.1862,0.14844-0.37826,0.26009-0.57617,0.33496-0.19792,0.07487-0.40104,0.1123-0.60938,0.1123-0.28125,0-0.53678-0.060221-0.7666-0.18066-0.23-0.1204-0.4035-0.2946-0.5207-0.5224-0.1172-0.2279-0.1758-0.4825-0.1758-0.7637-3E-7-0.27864,0.058268-0.53874,0.1748-0.78027,0.11654-0.24153,0.28418-0.4209,0.50293-0.53809,0.21875-0.11718,0.4707-0.17578,0.75586-0.17578,0.20703,0.0000029,0.3942,0.033532,0.56152,0.10059,0.16732,0.06706,0.2985,0.16048,0.39355,0.28027s0.16732,0.27604,0.2168,0.46875l-0.3418,0.09375c-0.04297-0.14583-0.09636-0.26041-0.16016-0.34375-0.0638-0.083331-0.15495-0.15006-0.27344-0.2002-0.11849-0.050128-0.25-0.075193-0.39453-0.075195-0.17318,0.0000026-0.32292,0.02637-0.44922,0.079102-0.1263,0.052737-0.22819,0.12207-0.30566,0.20801-0.07748,0.08594-0.1377,0.18034-0.18066,0.2832-0.072917,0.17709-0.10938,0.36914-0.10938,0.57617-6E-7,0.25521,0.043945,0.46875,0.13184,0.64062,0.087889,0.17188,0.21582,0.29948,0.38379,0.38281,0.16797,0.083334,0.34635,0.125,0.53516,0.125,0.16406,3E-7,0.32422-0.031575,0.48047-0.094727,0.15625-0.063151,0.27474-0.13053,0.35547-0.20215v-0.5332z""/>
            // </g>
            // <g fill=""#000"">
            //  <path d=""m5.1456,8.9503,0-2.0742,0.31641,0,0,0.29492c0.1524-0.2279,0.3724-0.3418,0.6602-0.3418,0.125,0.0000022,0.23991,0.022463,0.34473,0.067383,0.10482,0.044924,0.18327,0.10384,0.23535,0.17676,0.052081,0.072919,0.08854,0.15951,0.10938,0.25977,0.013019,0.065106,0.019529,0.17904,0.019531,0.3418v1.2754h-0.35156v-1.2617c0.0001-0.1434-0.0136-0.2505-0.0409-0.3214-0.0273-0.071-0.0758-0.1276-0.1455-0.17-0.0697-0.0423-0.1514-0.0634-0.2451-0.0634-0.1498,0-0.279,0.0475-0.3877,0.1425-0.1087,0.0951-0.1631,0.2754-0.1631,0.5411v1.1328z""/>
            //  <path d=""M7.2413,7.9132c0-0.3842,0.1068-0.6687,0.3204-0.8536,0.1783-0.1536,0.3958-0.2304,0.6523-0.2304,0.28515,0.0000022,0.51823,0.093427,0.69922,0.28027,0.18099,0.18685,0.27148,0.44499,0.27148,0.77441-0.000002,0.26693-0.040041,0.47689-0.12012,0.62988-0.0801,0.1529-0.1966,0.2718-0.3496,0.3564s-0.32,0.1269-0.501,0.1269c-0.2904,0-0.5251-0.0931-0.7041-0.2793-0.179-0.1861-0.2686-0.4544-0.2686-0.8046zm0.36133,0c-5E-7,0.26563,0.057942,0.46452,0.17383,0.59668,0.11588,0.13216,0.26172,0.19824,0.4375,0.19824,0.17448,2E-7,0.31966-0.066406,0.43555-0.19922,0.116-0.1328,0.174-0.3353,0.174-0.6075,0-0.2565-0.0583-0.4508-0.1748-0.583-0.1166-0.1321-0.2614-0.1982-0.4346-0.1982-0.17578,0.0000019-0.32162,0.065757-0.4375,0.19727-0.11589,0.13151-0.17383,0.33008-0.17383,0.5957z"" />
            //  <path d=""m10.366,8.6358,0.05078,0.31055c-0.09896,0.020833-0.1875,0.03125-0.26562,0.03125-0.1276,0-0.22656-0.020182-0.29688-0.060547-0.0698-0.0404-0.1193-0.0935-0.1479-0.1592-0.0287-0.0658-0.043-0.2041-0.043-0.4151v-1.1934h-0.25781v-0.27344h0.25781v-0.51367l0.34961-0.21094v0.72461h0.35352v0.27344h-0.35352v1.2129c-0.000001,0.10026,0.0062,0.16471,0.01856,0.19336,0.01237,0.028646,0.03255,0.051433,0.06055,0.068359,0.02799,0.016927,0.06803,0.025391,0.12012,0.025391,0.03906,3E-7,0.09049-0.00456,0.1543-0.013672z""/>
            // </g>
            // <g fill=""#000"">
            //  <path d=""m3.1955,13.086,0-1.8008-0.31055,0,0-0.27344,0.31055,0,0-0.2207c-4E-7-0.13932,0.012369-0.24284,0.037109-0.31055,0.033854-0.09114,0.093424-0.16504,0.17871-0.22168s0.20475-0.08496,0.3584-0.08496c0.098957,0.000003,0.20833,0.01172,0.32812,0.03516l-0.052734,0.30664c-0.072918-0.01302-0.14193-0.01953-0.20703-0.01953-0.10677,0.000003-0.18229,0.02279-0.22656,0.06836-0.044272,0.04557-0.066407,0.13086-0.066406,0.25586v0.19141h0.4043v0.27344h-0.4043v1.8008z""/>
            //  <path d=""m4.09,12.049c-1E-7-0.38411,0.10677-0.66862,0.32031-0.85352,0.17838-0.15364,0.39583-0.23047,0.65234-0.23047,0.28515,0.000002,0.51823,0.09343,0.69922,0.28027,0.18099,0.18685,0.27148,0.44499,0.27148,0.77441-0.0000021,0.26693-0.040041,0.47689-0.12012,0.62988-0.08008,0.153-0.19662,0.27181-0.34961,0.35644-0.153,0.08463-0.31999,0.12695-0.50098,0.12695-0.29037,0-0.52507-0.0931-0.7041-0.2793-0.179-0.185-0.2685-0.453-0.2685-0.804zm0.36133,0c-4E-7,0.26562,0.057942,0.46452,0.17383,0.59668,0.11588,0.13216,0.26172,0.19824,0.4375,0.19824,0.17448,0.000001,0.31966-0.06641,0.43555-0.19922,0.11588-0.13281,0.17383-0.33529,0.17383-0.60742-0.0000017-0.25651-0.05827-0.45084-0.1748-0.58301-0.11654-0.13216-0.26139-0.19824-0.43457-0.19824-0.17578,0.000002-0.32162,0.06576-0.4375,0.19727-0.11589,0.13151-0.17383,0.33008-0.17383,0.5957z"" />
            //  <path d=""m7.8068,13.086,0-0.30469c-0.16146,0.23438-0.38086,0.35156-0.6582,0.35156-0.1224,0-0.23665-0.02344-0.34277-0.07031s-0.1849-0.10579-0.23633-0.17676c-0.051433-0.07096-0.087565-0.15788-0.1084-0.26074-0.014323-0.06901-0.021485-0.17838-0.021484-0.32812v-1.2852h0.35156v1.1504c-6E-7,0.18359,0.00716,0.30729,0.021484,0.37109,0.022135,0.09245,0.06901,0.16504,0.14062,0.21777,0.071614,0.05274,0.16016,0.0791,0.26562,0.0791,0.10547,0,0.20443-0.02702,0.29688-0.08106,0.092446-0.05404,0.15788-0.1276,0.19629-0.2207s0.057616-0.22819,0.057617-0.40527v-1.1113h0.35156v2.0742z""/>
            //  <path d=""m8.674,13.086,0-2.0742,0.31641,0,0,0.29492c0.15234-0.22786,0.37239-0.3418,0.66016-0.3418,0.125,0.000002,0.23991,0.02246,0.34473,0.06738s0.18327,0.10384,0.23535,0.17676,0.08854,0.15951,0.10938,0.25977c0.01302,0.06511,0.01953,0.17904,0.01953,0.3418v1.2754h-0.35156v-1.2617c-0.000002-0.14323-0.013673-0.25032-0.041016-0.32129-0.027345-0.07096-0.075848-0.1276-0.14551-0.16992-0.069663-0.04232-0.15137-0.06347-0.24512-0.06348-0.14974,0.000002-0.27897,0.04753-0.3877,0.14258-0.10872,0.09505-0.16309,0.27539-0.16309,0.54102v1.1328z"" />
            //  <path d=""m12.246,13.086,0-0.26172c-0.13151,0.20573-0.32487,0.30859-0.58008,0.30859-0.16537,0-0.31738-0.04557-0.45606-0.13672-0.13867-0.09114-0.24609-0.21842-0.32227-0.38184-0.07617-0.16341-0.11426-0.35124-0.11426-0.56348,0-0.20703,0.03451-0.39486,0.10352-0.56348s0.17253-0.29785,0.31055-0.3877c0.13802-0.08984,0.29232-0.13476,0.46289-0.13477,0.125,0.000002,0.23633,0.02637,0.33398,0.0791,0.09765,0.05274,0.17708,0.12142,0.23828,0.20606v-1.0273h0.34961v2.8633zm-1.1113-1.0352c-0.000001,0.26563,0.05599,0.46419,0.16797,0.5957s0.24414,0.19727,0.39648,0.19726c0.15364,0.000001,0.28418-0.06283,0.3916-0.18848s0.16113-0.31738,0.16113-0.5752c-0.000001-0.28385-0.05469-0.49219-0.16406-0.625-0.10938-0.13281-0.24414-0.19922-0.4043-0.19922-0.15625,0.000002-0.28678,0.0638-0.3916,0.19141s-0.15723,0.32878-0.15723,0.60352z""/>
            // </g>
            //</svg>
            //";
            //            MySvg.Source = new Uri("string:" + testStringSvg);
            #endregion
        }
    }
}

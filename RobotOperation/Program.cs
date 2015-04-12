/**
 * アプリケーションのメイン関数を含むクラス。
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RobotOperation {
    static class Program {
        [STAThread]
        static void Main() {
            Console.WriteLine("Hello World using C#!");

            //微小歩行の各ノードのパラメータの設定。
            Dictionary<ServoTag, double> pos13 = new Dictionary<ServoTag,double>();
            pos13.Add(ServoTag.LEFT_HIP_ROLL, 150.0);
            pos13.Add(ServoTag.RIGHT_HIP_ROLL, 150.0);
            pos13.Add(ServoTag.LEFT_HIP_PITCH, 500.0);
            pos13.Add(ServoTag.RIGHT_HIP_PITCH, -500.0);
            pos13.Add(ServoTag.LEFT_KNEE, 1000.0);
            pos13.Add(ServoTag.RIGHT_KNEE, -1000.0);
            pos13.Add(ServoTag.LEFT_ANKLE_PITCH, -650.0);
            pos13.Add(ServoTag.RIGHT_ANKLE_PITCH, 650.0);
            pos13.Add(ServoTag.LEFT_ANKLE_ROLL, 150.0);
            pos13.Add(ServoTag.RIGHT_ANKLE_ROLL, 150.0);

            Dictionary<ServoTag, double> pos45 = new Dictionary<ServoTag, double>();
            pos45.Add(ServoTag.LEFT_HIP_ROLL, 400.0);
            pos45.Add(ServoTag.RIGHT_HIP_ROLL, 250.0);
            pos45.Add(ServoTag.LEFT_HIP_PITCH, 1100.0);
            pos45.Add(ServoTag.RIGHT_HIP_PITCH, -600.0);
            pos45.Add(ServoTag.LEFT_KNEE, 2200.0);
            pos45.Add(ServoTag.RIGHT_KNEE, -1200.0);
            pos45.Add(ServoTag.LEFT_ANKLE_PITCH, -1100.0);
            pos45.Add(ServoTag.RIGHT_ANKLE_PITCH, 750.0);
            pos45.Add(ServoTag.LEFT_ANKLE_ROLL, 450.0);
            pos45.Add(ServoTag.RIGHT_ANKLE_ROLL, 250.0);

            Dictionary<ServoTag, double> pos7 = new Dictionary<ServoTag, double>();
            pos7.Add(ServoTag.LEFT_HIP_ROLL, 0);
            pos7.Add(ServoTag.RIGHT_HIP_ROLL, 0);
            pos7.Add(ServoTag.LEFT_HIP_PITCH, 900.0);
            pos7.Add(ServoTag.RIGHT_HIP_PITCH, -500.0);
            pos7.Add(ServoTag.LEFT_KNEE, 1200.0);
            pos7.Add(ServoTag.RIGHT_KNEE, -1200.0);
            pos7.Add(ServoTag.LEFT_ANKLE_PITCH, -450.0);
            pos7.Add(ServoTag.RIGHT_ANKLE_PITCH, 900.0);
            pos7.Add(ServoTag.LEFT_ANKLE_ROLL, 0);
            pos7.Add(ServoTag.RIGHT_ANKLE_ROLL, -100.0);

            Dictionary<ServoTag, double> pos9 = new Dictionary<ServoTag, double>();
            pos9.Add(ServoTag.LEFT_HIP_ROLL, -100.0);
            pos9.Add(ServoTag.RIGHT_HIP_ROLL, -50.0);
            pos9.Add(ServoTag.LEFT_HIP_PITCH, 500.0);
            pos9.Add(ServoTag.RIGHT_HIP_PITCH, 250.0);
            pos9.Add(ServoTag.LEFT_KNEE, 1200.0);
            pos9.Add(ServoTag.RIGHT_KNEE, -700.0);
            pos9.Add(ServoTag.LEFT_ANKLE_PITCH, -800.0);
            pos9.Add(ServoTag.RIGHT_ANKLE_PITCH, 900.0);
            pos9.Add(ServoTag.LEFT_ANKLE_ROLL, -200.0);
            pos9.Add(ServoTag.RIGHT_ANKLE_ROLL, -200.0);

            Dictionary<ServoTag, double> pos33 = new Dictionary<ServoTag, double>();
            pos33.Add(ServoTag.LEFT_HIP_ROLL, -250.0);
            pos33.Add(ServoTag.RIGHT_HIP_ROLL, -250.0);
            pos33.Add(ServoTag.LEFT_HIP_PITCH, 600.0);
            pos33.Add(ServoTag.RIGHT_HIP_PITCH, -300.0);
            pos33.Add(ServoTag.LEFT_KNEE, 1200.0);
            pos33.Add(ServoTag.RIGHT_KNEE, -1700.0);
            pos33.Add(ServoTag.LEFT_ANKLE_PITCH, -700.0);
            pos33.Add(ServoTag.RIGHT_ANKLE_PITCH, 1000.0);
            pos33.Add(ServoTag.LEFT_ANKLE_ROLL, -300.0);
            pos33.Add(ServoTag.RIGHT_ANKLE_ROLL, -300.0);

            Dictionary<ServoTag, double> pos0 = new Dictionary<ServoTag, double>();
            pos0.Add(ServoTag.LEFT_HIP_ROLL, -250.0);
            pos0.Add(ServoTag.RIGHT_HIP_ROLL, -400.0);
            pos0.Add(ServoTag.LEFT_HIP_PITCH, 600.0);
            pos0.Add(ServoTag.RIGHT_HIP_PITCH, -1100.0);
            pos0.Add(ServoTag.LEFT_KNEE, 1200.0);
            pos0.Add(ServoTag.RIGHT_KNEE, -2200.0);
            pos0.Add(ServoTag.LEFT_ANKLE_PITCH, -700.0);
            pos0.Add(ServoTag.RIGHT_ANKLE_PITCH, 1100.0);
            pos0.Add(ServoTag.LEFT_ANKLE_ROLL, -250.0);
            pos0.Add(ServoTag.RIGHT_ANKLE_ROLL, -450.0);

            Dictionary<ServoTag, double> pos8 = new Dictionary<ServoTag, double>();
            pos8.Add(ServoTag.LEFT_HIP_ROLL, 0);
            pos8.Add(ServoTag.RIGHT_HIP_ROLL, 0);
            pos8.Add(ServoTag.LEFT_HIP_PITCH, 500.0);
            pos8.Add(ServoTag.RIGHT_HIP_PITCH, -900.0);
            pos8.Add(ServoTag.LEFT_KNEE, 1200.0);
            pos8.Add(ServoTag.RIGHT_KNEE, -1200.0);
            pos8.Add(ServoTag.LEFT_ANKLE_PITCH, -900.0);
            pos8.Add(ServoTag.RIGHT_ANKLE_PITCH, 450.0);
            pos8.Add(ServoTag.LEFT_ANKLE_ROLL, 100.0);
            pos8.Add(ServoTag.RIGHT_ANKLE_ROLL, 0);

            ServoManager servoManager = new ServoManager();
        }

    }
}
/*
*/
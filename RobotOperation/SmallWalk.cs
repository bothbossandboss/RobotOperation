using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotOperation {
    class SmallWalk {
        public Dictionary<ServoTag, double> posFirst;
        public Dictionary<ServoTag, double> pos13;
        public Dictionary<ServoTag, double> pos45;
        public Dictionary<ServoTag, double> pos7;
        public Dictionary<ServoTag, double> pos9;
        public Dictionary<ServoTag, double> pos33;
        public Dictionary<ServoTag, double> pos0;
        public Dictionary<ServoTag, double> pos8;
        public Dictionary<ServoTag, double> pos22;
        public Dictionary<ServoTag, double> pos4;
        public Dictionary<ServoTag, double> pos1;
        public Dictionary<ServoTag, double> pos43;
        public Dictionary<ServoTag, double> pos44;
        public Dictionary<ServoTag, double> posFin;
        public Dictionary<string, Dictionary<ServoTag, double>> posDic;
        public Dictionary<string, int> frameDic;

        public SmallWalk() {
            //微小歩行の各ノードのパラメータの設定。
            posFirst = new Dictionary<ServoTag, double>();
            posFirst.Add(ServoTag.LEFT_HIP_ROLL, 0);
            posFirst.Add(ServoTag.RIGHT_HIP_ROLL, 0);
            posFirst.Add(ServoTag.LEFT_HIP_PITCH, 0);
            posFirst.Add(ServoTag.RIGHT_HIP_PITCH, 0);
            posFirst.Add(ServoTag.LEFT_KNEE, 0);
            posFirst.Add(ServoTag.RIGHT_KNEE, 0);
            posFirst.Add(ServoTag.LEFT_ANKLE_PITCH, 0);
            posFirst.Add(ServoTag.RIGHT_ANKLE_PITCH, 0);
            posFirst.Add(ServoTag.LEFT_ANKLE_ROLL, 0);
            posFirst.Add(ServoTag.RIGHT_ANKLE_ROLL, 0);

            pos13 = new Dictionary<ServoTag, double>();
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

            pos45 = new Dictionary<ServoTag, double>();
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

            pos7 = new Dictionary<ServoTag, double>();
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

            pos9 = new Dictionary<ServoTag, double>();
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

            pos33 = new Dictionary<ServoTag, double>();
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

            pos0 = new Dictionary<ServoTag, double>();
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

            pos8 = new Dictionary<ServoTag, double>();
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

            pos22 = new Dictionary<ServoTag, double>();
            pos22.Add(ServoTag.LEFT_HIP_ROLL, 50.0);
            pos22.Add(ServoTag.RIGHT_HIP_ROLL, 100.0);
            pos22.Add(ServoTag.LEFT_HIP_PITCH, -250.0);
            pos22.Add(ServoTag.RIGHT_HIP_PITCH, -500.0);
            pos22.Add(ServoTag.LEFT_KNEE, 700.0);
            pos22.Add(ServoTag.RIGHT_KNEE, -1200.0);
            pos22.Add(ServoTag.LEFT_ANKLE_PITCH, -900.0);
            pos22.Add(ServoTag.RIGHT_ANKLE_PITCH, 800.0);
            pos22.Add(ServoTag.LEFT_ANKLE_ROLL, 200.0);
            pos22.Add(ServoTag.RIGHT_ANKLE_ROLL, 200.0);

            pos4 = new Dictionary<ServoTag, double>();
            pos4.Add(ServoTag.LEFT_HIP_ROLL, 250.0);
            pos4.Add(ServoTag.RIGHT_HIP_ROLL, 250.0);
            pos4.Add(ServoTag.LEFT_HIP_PITCH, 300.0);
            pos4.Add(ServoTag.RIGHT_HIP_PITCH, -600.0);
            pos4.Add(ServoTag.LEFT_KNEE, 1700.0);
            pos4.Add(ServoTag.RIGHT_KNEE, -1200.0);
            pos4.Add(ServoTag.LEFT_ANKLE_PITCH, -1000.0);
            pos4.Add(ServoTag.RIGHT_ANKLE_PITCH, 700.0);
            pos4.Add(ServoTag.LEFT_ANKLE_ROLL, 300.0);
            pos4.Add(ServoTag.RIGHT_ANKLE_ROLL, 300.0);

            pos1 = new Dictionary<ServoTag, double>();
            pos1.Add(ServoTag.LEFT_HIP_ROLL, 400.0);
            pos1.Add(ServoTag.RIGHT_HIP_ROLL, 250.0);
            pos1.Add(ServoTag.LEFT_HIP_PITCH, 1100.0);
            pos1.Add(ServoTag.RIGHT_HIP_PITCH, -600.0);
            pos1.Add(ServoTag.LEFT_KNEE, 2200.0);
            pos1.Add(ServoTag.RIGHT_KNEE, -1200.0);
            pos1.Add(ServoTag.LEFT_ANKLE_PITCH, -1100.0);
            pos1.Add(ServoTag.RIGHT_ANKLE_PITCH, 700.0);
            pos1.Add(ServoTag.LEFT_ANKLE_ROLL, 450.0);
            pos1.Add(ServoTag.RIGHT_ANKLE_ROLL, 250.0);

            pos43 = new Dictionary<ServoTag, double>();
            pos43.Add(ServoTag.LEFT_HIP_ROLL, -250.0);
            pos43.Add(ServoTag.RIGHT_HIP_ROLL, -250.0);
            pos43.Add(ServoTag.LEFT_HIP_PITCH, 600.0);
            pos43.Add(ServoTag.RIGHT_HIP_PITCH, -900.0);
            pos43.Add(ServoTag.LEFT_KNEE, 1200.0);
            pos43.Add(ServoTag.RIGHT_KNEE, -2100.0);
            pos43.Add(ServoTag.LEFT_ANKLE_PITCH, -700.0);
            pos43.Add(ServoTag.RIGHT_ANKLE_PITCH, 1300.0);
            pos43.Add(ServoTag.LEFT_ANKLE_ROLL, -300.0);
            pos43.Add(ServoTag.RIGHT_ANKLE_ROLL, -300.0);

            pos44 = new Dictionary<ServoTag, double>();
            pos44.Add(ServoTag.LEFT_HIP_ROLL, 250.0);
            pos44.Add(ServoTag.RIGHT_HIP_ROLL, 250.0);
            pos44.Add(ServoTag.LEFT_HIP_PITCH, 900.0);
            pos44.Add(ServoTag.RIGHT_HIP_PITCH, -600.0);
            pos44.Add(ServoTag.LEFT_KNEE, 2100.0);
            pos44.Add(ServoTag.RIGHT_KNEE, -1200.0);
            pos44.Add(ServoTag.LEFT_ANKLE_PITCH, -1300.0);
            pos44.Add(ServoTag.RIGHT_ANKLE_PITCH, 700.0);
            pos44.Add(ServoTag.LEFT_ANKLE_ROLL, 300.0);
            pos44.Add(ServoTag.RIGHT_ANKLE_ROLL, 300.0);

            posFin = new Dictionary<ServoTag, double>();
            posFin.Add(ServoTag.LEFT_HIP_ROLL, 0);
            posFin.Add(ServoTag.RIGHT_HIP_ROLL, 0);
            posFin.Add(ServoTag.LEFT_HIP_PITCH, 500.0);
            posFin.Add(ServoTag.RIGHT_HIP_PITCH, -500.0);
            posFin.Add(ServoTag.LEFT_KNEE, 1000.0);
            posFin.Add(ServoTag.RIGHT_KNEE, -1000.0);
            posFin.Add(ServoTag.LEFT_ANKLE_PITCH, -600.0);
            posFin.Add(ServoTag.RIGHT_ANKLE_PITCH, 600.0);
            posFin.Add(ServoTag.LEFT_ANKLE_ROLL, 0);
            posFin.Add(ServoTag.RIGHT_ANKLE_ROLL, 0);

            posDic = new Dictionary<string, Dictionary<ServoTag, double>>();
            posDic.Add("posFirst", posFirst);
            posDic.Add("pos13", pos13);
            posDic.Add("pos45", pos45);
            posDic.Add("pos7", pos7);
            posDic.Add("pos9", pos9);
            posDic.Add("pos33", pos33);
            posDic.Add("pos0", pos0);
            posDic.Add("pos8", pos8);
            posDic.Add("pos22", pos22);
            posDic.Add("pos4", pos4);
            posDic.Add("pos1", pos1);
            posDic.Add("pos43", pos43);
            posDic.Add("pos44", pos44);
            posDic.Add("posFin", posFin);

            frameDic = new Dictionary<string, int>();
            frameDic.Add("pos13", 20);
            frameDic.Add("pos45", 12);
            frameDic.Add("pos7", 17);
            frameDic.Add("pos9", 20);
            frameDic.Add("pos33", 20);
            frameDic.Add("pos0", 12);
            frameDic.Add("pos8", 17);
            frameDic.Add("pos22", 20);
            frameDic.Add("pos4", 20);
            frameDic.Add("pos1", 12);
            frameDic.Add("pos43", 15);
            frameDic.Add("pos44", 15);
            frameDic.Add("posFin", 30);
        }
    }
}

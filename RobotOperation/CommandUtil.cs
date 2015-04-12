/**
 * 便利なメソッドを集めたクラス？
 * 城さんの作ったクラスをそのままコピー。
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotOperation {
    class CommandUtil {
        //ServoManager.csのServoDataクラスに上限下限を自分で設定できるメソッドがあるので、使われてない？
        public static int servoAngleConvert(double angle) {
            //7500のとき0deg            
            int neutral = 7500;       
            //±135deg => ±4000 
            int servoAngle = neutral + (int)((angle / 135.0) * 4000);

            //サーボモーター最大角、最小角の設定
            //3500より小さいときは3500にする
            if (servoAngle < 3500) servoAngle = 3500;      

            //11500より大きいときは11500にする
            if (servoAngle > 11500) servoAngle = 11500;     

            return servoAngle; 
        }

        public static byte[] SeriesServoMove(Dictionary<int, int> dest, int frame) {
            int cnt = dest.Count();
            byte size = (byte)(8 + 2 * cnt + 1);
            byte[] cmd = new byte[size];
            cmd[0] = size;
            cmd[1] = 0x10;
            cmd[7] = (byte)frame;

            int selectIndex = 2;
            int destIndex = 8;
            int servoCnt = 0;
            foreach (int key in dest.Keys) {
                int quot = (int)(key / 8);
                int offset = key % 8;
                cmd[selectIndex + quot] += (byte)(System.Math.Pow(2, offset));

                byte[] destByte = BitConverter.GetBytes((short)dest[key]);
                cmd[destIndex + servoCnt * 2] = destByte[0];
                cmd[destIndex + servoCnt * 2 + 1] = destByte[1];
                servoCnt++;
            }

            byte sum = 0;
            for (int i = 0; i < size - 1; i++) {
                sum += cmd[i];
            }
            cmd[size - 1] = sum;
            return cmd;
        }

    }
}
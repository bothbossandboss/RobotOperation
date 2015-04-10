/**
 * KHR-3HVの各サーボの角度を指定するクラス。
 * kinect  v2で取得した骨格の角度をサーボの角度に変換して、シリアル通信用送信データも作成する。
 * 2015/4/10時点ではkinect関連は考慮していない。
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace RobotOperation {
    /**
     * 各サーボのID
     */
    public enum ServoTag {
        NECK = 0, //首(壊れている?)
        WAIST, //腰(回転方向不明)
        LEFT_SHOULDER_PITCH, //左肩ピッチ方向回転
        RIGHT_SHOULDER_PITCH, //右肩ピッチ方向回転
        LEFT_SHOULDER_ROLL, //左肩ロール回転(外側に開く)
        RIGHT_SHOULDER_ROLL, //右肩ロール回転(内側に閉じる(鏡面対称ではない))
        LEFT_SHOULDER_YAW, //左肩ヨー方向回転
        RIGHT_SHOULDER_YAW, //右肩ヨー方向回転
        LEFT_ELBOW, //左肘
        RIGHT_ELBOW, //右肘
        LEFT_HIP_YAW, //左股間節ヨー方向回転
        RIGHT_HIP_YAW, //右股間節ヨー方向回転
        LEFT_HIP_ROLL, //左股関節ロール方向回転
        RIGHT_HIP_ROLL, //右股関節ロール方向回転
        LEFT_HIP_PITCH, //左股関節ピッチ方向回転
        RIGHT_HIP_PITCH, //右股関節ピッチ方向回転
        LEFT_KNEE, //左膝
        RIGHT_KNEE, //右膝
        LEFT_ANKLE_PITCH, //左足首ピッチ方向回転
        RIGHT_ANKLE_PITCH, //右足首ピッチ方向回転
        LEFT_ANKLE_ROLL, //左足首ロール方向回転
        RIGHT_ANKLE_ROLL, //右足首ロール方向回転
        NUM_OF_SERVO
    }

    /**
     * 各サーボの角度などを格納するクラス。
     */
    public class ServoData {
        public bool changeFlag; //変更フラグ
        public bool direction; //方向の正負
        public double destAngle; //目標角度 [rad]
        public int trim; //トリム
        public int upperLimit; //上限
        public int lowerLimit; //下限
        public int offset; //オフセット

        public ServoData(bool dir, int t, int llim, int ulim) {
            changeFlag = false;
            destAngle = 0.0;
            direction = dir;
            trim = t;
            upperLimit = ulim;
            lowerLimit = llim;
            offset = 0;
        }

        public ServoData(bool dir, int t, int llim, int ulim, int ofs) {
            changeFlag = false;
            destAngle = 0.0;
            direction = dir;
            trim = t;
            upperLimit = ulim;
            lowerLimit = llim;
            offset = ofs;
        }

        public ServoData(bool dir, int t) {
            changeFlag = false;
            destAngle = 0.0;
            direction = dir;
            trim = t;
            upperLimit = 11500;
            lowerLimit = 3500;
        }

        public void setDest(double d) {
            changeFlag = (d != destAngle);
            destAngle = d;
        }

        //初期値を考慮した角度指定？
        public int toServoAngle(double angle) {
            //7500のとき0deg            
            int neutral = 7500;
            //±135deg =&gt;±4000 
            int servoAngle = neutral + (int)((angle / 135.0) * 4000);
            Debug.WriteLine("servoangle : {0}", servoAngle);
            //サーボモーター最大角、最小角の設定
            //下限より小さいときは下限にする
            if (servoAngle < lowerLimit) {
                servoAngle = lowerLimit;
            }
            Debug.WriteLine("servoangle : {0}", servoAngle);
            //上限より大きいときは上限にする
            if (servoAngle > upperLimit) {
                servoAngle = upperLimit;
            }
            Debug.WriteLine("servoangle : {0}", servoAngle);

            return (int)servoAngle;
        }
    }

    /**
     * KHR-3HVのサーボを管理するクラス。
     */
    class ServoManager {
        //各サーボのIDとデータを管理する連想配列。
        private Dictionary<ServoTag, ServoData> servoDict;

        public ServoManager() {
            servoDict = new Dictionary<ServoTag, ServoData>();
            servoDict.Add(ServoTag.WAIST, new ServoData(true, 0, 5500, 9500));
            servoDict.Add(ServoTag.LEFT_SHOULDER_PITCH, new ServoData(true, -1350));
            servoDict.Add(ServoTag.RIGHT_SHOULDER_PITCH, new ServoData(false, 900));
            servoDict.Add(ServoTag.LEFT_SHOULDER_ROLL, new ServoData(true, -2650, 7500, 12800, -10));
            servoDict.Add(ServoTag.RIGHT_SHOULDER_ROLL, new ServoData(false, 2650, 2200, 7500, -10));
            servoDict.Add(ServoTag.LEFT_SHOULDER_YAW, new ServoData(false, 0));
            servoDict.Add(ServoTag.RIGHT_SHOULDER_YAW, new ServoData(true, 0));
            servoDict.Add(ServoTag.LEFT_ELBOW, new ServoData(false, 2650, 3800, 8400));
            servoDict.Add(ServoTag.RIGHT_ELBOW, new ServoData(true, -2650, 6600, 11200));
            servoDict.Add(ServoTag.LEFT_KNEE, new ServoData(true, 0));
            servoDict.Add(ServoTag.RIGHT_KNEE, new ServoData(false, 0));
            servoDict.Add(ServoTag.LEFT_ANKLE_PITCH, new ServoData(true, 0));
            servoDict.Add(ServoTag.RIGHT_ANKLE_PITCH, new ServoData(false, 0));
            servoDict.Add(ServoTag.LEFT_ANKLE_ROLL, new ServoData(true, 0, 7500, 7900));
            servoDict.Add(ServoTag.RIGHT_ANKLE_ROLL, new ServoData(true, 0, 7100, 7500));
            servoDict.Add(ServoTag.LEFT_HIP_PITCH, new ServoData(false, 0));
            servoDict.Add(ServoTag.RIGHT_HIP_PITCH, new ServoData(true, 0));
            servoDict.Add(ServoTag.LEFT_HIP_ROLL, new ServoData(true, 0, 7500, 7900));
            servoDict.Add(ServoTag.RIGHT_HIP_ROLL, new ServoData(true, 0, 7100, 7500));
            servoDict.Add(ServoTag.NECK, new ServoData(true, 0, 6800, 8200));
        }

        public Dictionary<ServoTag, ServoData> getServo() {
            return servoDict;
        }

        /**
         * 城さんのプログラムでは、kinect v2から骨格の角度を割り出し、 
         * public void calcServoDest(Dictionary<BoneTag, Vector3D> boneDict)
         * でservoDictの値を更新していた。
         * 2015/4/10時点ではservoDictを更新するメソッドは追加していない。
         */

        //サーボ角度表記へ変換
        private Dictionary<int, int> servoToDest(Dictionary<ServoTag, ServoData> d) {
            Dictionary<int, int> ret = new Dictionary<int, int>(d.Count);
            foreach (ServoTag s in servoDict.Keys) {
                if (d[s].changeFlag) {
                    Debug.WriteLine("servo ID {0}", (int)s);
                    if (d[s].direction) {
                        ret.Add((int)s, d[s].toServoAngle(d[s].destAngle));
                    }else {
                        ret.Add((int)s, d[s].toServoAngle(-d[s].destAngle));
                    }
                }
            }
            return ret;
        }

        //シリアル通信で送るべき、各サーボの角度指令を連想配列から生成。
        public byte[] generateCommand() {
            byte[] cmd = CommandUtil.SeriesServoMove(servoToDest(servoDict), 25);
            return cmd;
        }
    }
}

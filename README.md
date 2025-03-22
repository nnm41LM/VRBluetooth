# Meta Quest と M5Stack を Bluetooth で連携

![VR Bluetooth Video](images/VRBluetooth.gif)  

## 🎯 概要

このプロジェクトは、**M5Stack から Bluetooth を通じて送信されたデータを Meta Quest 上の Unity アプリで受信し、VR空間内にテキストとして表示するシステム** です。

📌 **Bluetooth 通信でハードウェアとVRをつなげる実験的アプローチ**  
📌 **M5Stack 側でセンサーデータや入力を送信し、Quest 側でリアルタイムに受信表示**

---

## 🔧 実装構成

### 🔌 M5Stack からの Bluetooth 送信
- **Arduino IDE** を使用して、M5Stack の Bluetooth モジュールから任意の信号を送信
- センサー値や固定データなど、用途に応じた拡張が可能

### 🎮 Meta Quest での Bluetooth 受信
- **BlueUnity ライブラリ** を使用し、Android Bluetooth API を経由して M5Stack と接続
- 受信したデータを **VR空間内にリアルタイム表示**

---

## 🛠 使用技術
| 技術 | 詳細 |
|------|------|
| Unity | 2022.3.16f1 |
| C# | .NET Standard |
| BlueUnity | 3.0.0 |
| Arduino IDE | 1.8.15 |
| M5Stack | Core2 / Basic など |

---

## 📥 アプリの使用方法

1. Quest の **開発者モードを有効化**
2. `build` フォルダにある `apk` を **Meta Quest 2 / 3 にインストール**
3. M5Stack 側に `ino` ファイルを書き込み、電源を入れて Bluetooth ペアリング
4. アプリを起動すると、Bluetooth 経由で受信した文字列が VR 上に表示されます

---

## 🎮 確認済み動作環境

✅ **Meta Quest 2**  
✅ **Meta Quest 3**

---

## 🛠 技術的工夫

📌 **BlueUnity を使ったシンプルな Bluetooth 通信実装**  
📌 **コントローラーを使わず、外部デバイスから VR 体験を制御**  
📌 **今後の応用例として、センサー連携・ゲームの入力デバイスなどにも展開可能**

---

## 📚 参考資料

🔗 **ドキュメント**
- [📄 Meta XRパッケージドキュメント](https://developers.meta.com/horizon/documentation/unity/unity-package-manager/?locale=ja_JP)  
- [📄 BlueUnity GitHub リポジトリ](https://github.com/bentalebahmed/BlueUnity)

🔗 **参考リンク**
- [📖 M5Stack 開発環境のセットアップ（IIJ）](https://manual.iij.jp/iot/devices/42076478.html)  

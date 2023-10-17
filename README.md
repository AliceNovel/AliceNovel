# Alice Novel

## 概要
こちらはノベルゲーム向けのゲームエンジンとなっています。<br />
こちらのゲームエンジンを採用することにより、プログラムやUIの作成の必要がなくなるため、ゲームの作成が容易になります。<br />

### 紹介サイト
**[Alice Novel公式サイト](https://alicenovel.web.app "Alice Novel で世界をより楽しく")**<br />

### 技術
フレームワーク<br />
- [MAUI] / [.NET7.0]

プログラミング言語/デザイン<br />
- [C#] + [xaml]

[MAUI]: https://dotnet.microsoft.com/ja-jp/apps/maui ".NET MAUI"
[.NET7.0]: https://dotnet.microsoft.com/ja-jp/ ".NET"
[C#]: https://learn.microsoft.com/ja-jp/dotnet/csharp/ "C#ドキュメント"
[xaml]: https://learn.microsoft.com/ja-jp/dotnet/maui/xaml/ ".NET MAUI xamlドキュメント"

## 開発進捗
状況: **開発中**<br />
v0.9.0へ向けて誠意開発中です。<br />

### ロードマップ
現在予定している開発予定です。<br />
バージョンや実装に関しては前後したり、実装されなくなったりと変更になる可能性がありますのでご了承ください。<br />

#### v0.9.0
- ~~.anprojファイル読み込み~~ (v0.9.0-rc1)
- ~~画像/音声ファイル動作~~ (v0.9.0-rc1/v0.9.0-rc2)
- 音声ファイル読み込みの効率化/最終調整

#### v1.0.0
- 安定化

#### v1.0.0以降
- 設定画面/.anprojからUIのテーマカラーの変更
- ~~セーブ/ロード機能~~(v0.9.0にて簡易的に実装)
- 選択肢/シナリオ分岐
- プラグインシステム(.dllを使用する)
  - 戦闘プラグイン
  - アイテム管理プラグイン(プラグインとしてではなく、本体の機能として実装するかも)
- 動画ファイル動作

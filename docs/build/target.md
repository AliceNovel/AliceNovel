# Target OS

| TFM | Windows | MacOS | Linux |
| --- | --- | --- | --- |
| net8.0-android | :white_check_mark: | :white_check_mark: | :white_check_mark:[^1] |
| net8.0-ios | ⚠️[^2] | :white_check_mark: | :x: |
| net8.0-maccatalyst | ⚠️[^2] | :white_check_mark: | :x: |
| net8.0-windows | :white_check_mark: | :x: | :x: |

[^1]: It is nessesary to change `.csproj` file.
[^2]: Supporting only building, publishing is error.

---
*Reference*
- https://zenn.dev/proudust/articles/2022-12-06-build-maui-on-gh-actions

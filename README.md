﻿# AskMe

A simple program to prompt questions and dump answers to console as JSON.

Download binary files from [Releases](https://github.com/abdusco/askme/releases/latest)

## Usage

Each argument is parsed as a question. You can specify a default answer with `$question=$answer`

```powershell
./askme.exe "name" "age" "what year is it=2020"
```

![](askme.png)

Hit `[Enter]` or click **Save** to accept values. Hit `[ESC]` to cancel.

output: 

```json
{"name":"abdus","age":"27","what year is it":"2020"}
```

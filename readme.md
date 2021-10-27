<div align="center">
    <h1>
        Null.LibClassViewer
    </h1>
</div>



## 介绍

这是一个 .NET 程序集的类型查看器, 用于帮助开发者快速检索查阅程序集中的类型.



## 使用

在运行程序后, 即进入程序命令行

1. 使用 `LoadAssembly` 指令加载程序集

```
LoadAssembly assembly:String
```

2. 使用 ShowTypes 显示库中所有类型, 或显示与参数匹配的所有类型

```
ShowTypes
ShowTypes typeName:String fullName:Boolean
```

3. 使用 ShowSubTypes 显示与参数匹配类型的所有子类型

```
ShowSubTypes typeName:String fullName:Boolean
```

4. 使用 Help 指令查看程序指令简述

```
Help
Help cmdname:String
```



## 示例

```
>>> LoadAssembly ./TestAssembly.dll
. Assembly loaded, type count: 10

>>> ShowTypes
. Class1
. Class2
. Class3
. Class4
. Class5
. Class6
. Class7
. Class8
. Class9
. Class0

>>> ShowSubTypes Class1
. TestAssembly.Class1
. TestAssembly.Class2

```




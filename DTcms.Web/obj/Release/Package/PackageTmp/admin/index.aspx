<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="DTcms.Web.admin.index" %>

    <!DOCTYPE html>
    <html>

    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no"
        />
        <meta name="apple-mobile-web-app-capable" content="yes" />
        <title>后台管理中心</title>
        <link href="../scripts/artdialog/ui-dialog.css" rel="stylesheet" type="text/css" />
        <link href="skin/default/style.css" rel="stylesheet" type="text/css" />
        <script type="text/javascript" charset="utf-8" src="../scripts/jquery/jquery-1.11.2.min.js"></script>
        <script type="text/javascript" charset="utf-8" src="../scripts/jquery/jquery.nicescroll.js"></script>
        <script type="text/javascript" charset="utf-8" src="../scripts/artdialog/dialog-plus-min.js"></script>
        <script type="text/javascript" charset="utf-8" src="js/layindex.js"></script>
        <script type="text/javascript" charset="utf-8" src="js/common.js"></script>
    </head>
    <link rel="stylesheet" href="../../layui/css/layui.css">
    <script src="../../layui/layui.js"></script>
    <script src="../../layui/socket.io.js"></script>
    <script>
        if (!/^http(s*):\/\//.test(location.href)) {
            alert('请部署到localhost上查看该演示');
        }
        $(function () {
            //检测IE
            if ('undefined' == typeof (document.body.style.maxHeight)) {
                window.location.href = 'ie6update.html';
            }
        });
        var socket
        layui.use('layim', function (layim) {

            //演示自动回复
            var autoReplay = [
                '您好，我现在有事不在，一会再和您联系。',
                '你没发错吧？face[微笑] ',
                '洗澡中，请勿打扰，偷窥请购票，个体四十，团体八折，订票电话：一般人我不告诉他！face[哈哈] ',
                '你好，我是主人的美女秘书，有什么事就跟我说吧，等他回来我会转告他的。face[心] face[心] face[心] ',
                'face[威武] face[威武] face[威武] face[威武] ',
                '<（@￣︶￣@）>',
                '你要和我说话？你真的要和我说话？你确定自己想说吗？你一定非说不可吗？那你说吧，这是自动回复。',
                'face[黑线]  你慢慢说，别急……',
                '(*^__^*) face[嘻嘻] ，是贤心吗？'
            ];

            //基础配置
            layim.config({
                //初始化接口
                init: {
                    url: '../tools/admin_ajax.ashx?action=getuserlist&id=<%=admin_info.id %>&time=' + Math.random()
                    , data: {}
                }
                //查看群员接口
                , members: {
                    url: 'json/getMembers.json'
                    , data: {}
                }

                //上传图片接口
                , uploadImage: {
                    url: '../tools/upload_ajax.ashx?action=UpLoadFileIm' //（返回的数据格式见下文）
                    , type: '' //默认post
                }

                //上传文件接口
                , uploadFile: {
                    url: '../tools/upload_ajax.ashx?action=UpLoadFile' //（返回的数据格式见下文）
                    , type: '' //默认post
                }

                //扩展工具栏
                , tool: [{
                    alias: 'code'
                    , title: '代码'
                    , icon: '&#xe64e;'
                }]

                //,brief: true //是否简约模式（若开启则不显示主面板）

                , title: '<%=admin_info.real_name %>' //自定义主面板最小化时的标题
                //,right: '100px' //主面板相对浏览器右侧距离
                //,minRight: '90px' //聊天面板最小化时相对浏览器右侧距离
                , initSkin: '2.jpg' //1-5 设置初始背景
                , skin: ['aaa.jpg'] //新增皮肤
                //,isfriend: false //是否开启好友
                //,isgroup: false //是否开启群组
                //,min: true //是否始终最小化主面板，默认false
                , notice: true //是否开启桌面消息提醒，默认false
                //,voice: false //声音提醒，默认开启，声音文件为：default.wav

                //, msgbox: layui.cache.dir + 'css/modules/layim/html/msgbox.html' //消息盒子页面地址，若不开启，剔除该项即可
               // , find: layui.cache.dir + 'css/modules/layim/html/find.html' //发现页面地址，若不开启，剔除该项即可
               // , chatLog: layui.cache.dir + 'css/modules/layim/html/chatLog.html' //聊天记录页面地址，若不开启，剔除该项即可

            });

            //监听在线状态的切换事件
            layim.on('online', function (data) {
                //console.log(data);
                alert(data)
            });

            //监听签名修改
            layim.on('sign', function (value) {
                //console.log(value);
            });

            //监听自定义工具栏点击，以添加代码为例
            layim.on('tool(code)', function (insert) {
                layer.prompt({
                    title: '插入代码'
                    , formType: 2
                    , shade: 0
                }, function (text, index) {
                    layer.close(index);
                    insert('[pre class=layui-code]' + text + '[/pre]'); //将内容插入到编辑器
                });
            });

            //监听layim建立就绪
            layim.on('ready', function (res) {
                console.log(res);
                layim.msgbox(5); //模拟消息盒子有新消息，实际使用时，一般是动态获得
                socket = io.connect('http://139.129.229.214:3000');//与服务器进行连接
                var mydata = {
                    content: {
                        uid: <%=admin_info.id %>,
                        num: 1,
                    },
                    type: 'reg',
                };
                socket.emit('message', mydata);
                //接受消息（如果检测到该socket）
                socket.on('chatMessage', function (d) {
                    console.log(d)
                    /*处理单聊事件*/
                    if (d.type == 'friend') {
                        console.log(d)
                        layim.getMessage({
                            username: d.username
                            , avatar: d.avatar
                            , id: d.id
                            , type: "friend"
                            , content: d.content
                        });
                        /*处理群聊事件*/
                    } else if (d.content.to.type == 'group') {
                        mydata.id = mydata.toid;
                        socket.broadcast.emit('chatMessage', mydata)
                    }
                }, 3000);
                socket.on('setChatCache', function (d) {
                    console.log(d)
                    d.forEach(function(element) {
                        element=JSON.parse(element)
                        layim.getMessage({
                            username: element.username
                            , avatar: element.avatar
                            , id: element.id
                            , type: element.type
                            , content: element.content
                        });
                    }, this);
                    
                }, 3000)

            });
            //监听发送消息
            layim.on('sendMessage', function (data) {
                var To = data.to;
                console.log(data);
                var mydata = {
                    content: data,
                    type: 'chatMessage',
                };
                socket.emit('message', mydata);
                if (To.type === 'friend') {
                    layim.setChatStatus('<span style="color:#FF5722;">对方正在输入。。。</span>');
                }

            });

            //监听查看群员
            layim.on('members', function (data) {
                //console.log(data);
            });

            //监听聊天窗口的切换
            layim.on('chatChange', function (res) {
                var type = res.data.type;
                console.log(res.data.id)
                if (type === 'friend') {
                    //模拟标注好友状态
                    //layim.setChatStatus('<span style="color:#FF5722;">在线</span>');
                } else if (type === 'group') {
                    //模拟系统消息
                    layim.getMessage({
                        system: true
                        , id: res.data.id
                        , type: "group"
                        , content: '模拟群员' + (Math.random() * 100 | 0) + '加入群聊'
                    });
                }
            });



        });

    </script>

    <body class="indexbody">
        <form id="form1" runat="server">
            <!--全局菜单-->
            <a class="btn-paograms" onclick="togglePopMenu();"></a>
            <div id="pop-menu" class="pop-menu">
                <div class="pop-box">
                    <h1 class="title"><i></i>导航菜单</h1>
                    <i class="close" onclick="togglePopMenu();">关闭</i>
                    <div class="list-box"></div>
                </div>
                <i class="arrow">箭头</i>
            </div>
            <!--/全局菜单-->

            <div class="main-top">
                <a class="icon-menu"></a>
                <div id="main-nav" class="main-nav"></div>
                <div class="nav-right">
                    <div class="info">
                        <i></i>
                        <span>您好，<%=admin_info.user_name %><br>
                        <%=new DTcms.BLL.manager_role().GetTitle(admin_info.role_id) %>
                    </span>
                    </div>
                    <div class="option">
                        <i></i>
                        <div class="drop-wrap">
                            <div class="arrow"></div>
                            <ul class="item">
                                <li>
                                    <a href="../" target="_blank">预览网站</a>
                                </li>
                                <li>
                                    <a href="center.aspx" target="mainframe">管理中心</a>
                                </li>
                                <li>
                                    <a href="manager/manager_pwd.aspx" onclick="linkMenuTree(false, '');" target="mainframe">修改密码</a>
                                </li>
                                <li>
                                    <asp:LinkButton ID="lbtnExit" runat="server" OnClick="lbtnExit_Click">注销登录</asp:LinkButton>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>

            <div class="main-left">
                <h1 class="logo"></h1>
                <div id="sidebar-nav" class="sidebar-nav"></div>
            </div>

            <div class="main-container">
                <iframe id="mainframe" name="mainframe" frameborder="0" src="center.aspx"></iframe>
            </div>

        </form>
    </body>

    </html>
function Getconfig(type){

    $.ajax({
        url: "/Handle/Service/SetParameters.ashx",
        dataType: "json",
        data: {
            "type": type,
            "PlatIP": $("#PlatIP").val(),
            "PlatLoginName": $("#PlatLoginName").val(),
            "PlatLoginPWD": $("#PlatLoginPWD").val()
        }, success: function (data) {
           
            $("#PlatIP").val(data.result[0].PlatIP);
            $("#PlatLoginName").val(data.result[0].PlatLoginName);
            $("#PlatLoginPWD").val(data.result[0].PlatLoginPWD);
           // $("#myModalLabel").text("新增");
          //  clear();
            
        }
    });

}



$(function () {
    $('#edit_pwd').on('show.bs.modal', function () {
        Getconfig();
        $("#myModal").modal("hide");
    })

    $('#editconfig').on("click", function () {
           Getconfig("read");
    })

    $('#saveset').on("click", function () {
        Getconfig("update");
    })
    $('#gendanxitong').on("click", function () {
        $('#gendanxitong').modal("show");

    })

    $('#savepassword').on("click", function () {
       
        var _editoldpwd = $("#editoldpwd").val();
        var _password = $("#editpwd").val();
        var _confirmpassword = $("#editconfirmpwd").val();
        if (_editoldpwd == "") {
            createUserAlarm($("#editoldpwd"), "原密码不能为空");
  
            return;
        }
        if (_password == "") {
            createUserAlarm($("#editpwd"), "请输入新密码");

            return;
        }
        if (_password != _confirmpassword) {
            createUserAlarm($("#editconfirmpwd"), "二次密码不一样");

            return;
        }
        $.ajax({
            type: "get",
            url: "Handle/Service/Update_PWD.ashx",
            data: { 'pwd': _editoldpwd,'newpwd':_password },
            dataType: "json",
            success: function (data) {
                if (data.r == "0") {
                    //修改成功
                    alert("修改成功");
                    $("#edit_pwd").modal("hide");

                }
                else {
                    //修改失败

                    createUserAlarm($("#editconfirmpwd"),  data.result);
                }
            },
            error: function (msg) {
                alert(msg);
            }
        });

    })


   

});

function createUserAlarm($ele, txt) {
    var $doc = $ele;
    $doc.empty();
    $("#VerificationCode-error").remove();
    var _arlarmHtml = '<label id="VerificationCode-error" class="error"  style="display: inline-block;fontsize:12px;">' + txt + '</label>';
    $doc.addClass("input_danger");
    $doc.after(_arlarmHtml);
}
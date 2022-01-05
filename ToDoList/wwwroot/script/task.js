let $Page = {};

function GetDayList() {
    let uri = "api/task/day/list";
    $.ajax({
        url: uri,
        method: "GET"
    }).done(function (data) {
        renderList(data.Data);
    });

}

function renderList(data, append) {
    if (append == null) append = false;

    //console.log(data);
    let $table = $("#dataTable");
    if (!append) $table.html("");
    for (let i = 0; i < data.length; i++) {
        let $div = $(`<div class="row" style="padding:8px" data-key="${data[i].PK}"></div>`);
        $div.append(`<div class="col"><input type="checkbox" title="完成">&nbsp;<span onclick="ShowDetail(this);">${data[i].NM_SUBJECT}</span></div>`);
        $div.append(`<div class="col-lg-1"><button type="button">刪除</button></div>`);
        $table.append($div);
    }

    $Page["Data"] = data;
}



function getTask(k) {
    let task = null;
    if ($Page.Data) {
        for (let i = 0; i < $Page.Data.length; i++) {
            if ($Page.Data[i].PK == k) {
                task = $Page.Data[i];
                break;
            }
        }
    }

    return task;
}

function ShowDetail(dom) {
    $div = $(dom).parent().parent();
    let k = $div[0].dataset.key;
    //
    let task = getTask(k);
    if (task != null) {
        $("#divDetail").show();
        $("#NM_SUBJECT").text(task.NM_SUBJECT);
        $("#txtPreFinishDate").val("");
        $("#txtFinishDate").val(task.DT_FINISH);
        $("#txtMinute").val(task.QT_MINUTE | 0);

        $(".clock").html("00:00");

        if (task.QT_MINUTE != null)
            $("#btnbtnStartWork").hide();
        else
            $("#btnbtnStartWork").show();


    }
}

function btnHideDetail() {
    TargetTime = null;
    $("#divDetail").hide();
}

function btnStartWorkClick() {
    TargetTime = new Date();
    startTime();

    //寫入執行記錄
}


function SetTaskFinish(ck) {


}

function btnRemoveTaskClick(btn) {
    $div = $(dom).parent().parent();
    let k = $div[0].dataset.key;
    //
    let task = getTask(k);
    if (task != null) {
        console.log(task);

    }
}

function UpdateTask() {
    //更新內容

}

function btnAddTaskClick(btn) {
    $div = $(btn).parent().parent();
    $txt = $div.find("input[type='text']");
    if ($txt.val().trim() != "") {
        AddTask($txt.val().trim());
        $txt.val("");
    }
}


function AddTask(Subject, ParentKey) {
    let uri = "api/task";
    $.ajax({
        url: uri,
        type:"PUT",
        method: "PUT",
        dataType: 'json',
        contentType: 'application/json; charset=utf-8', 
        data: JSON.stringify({
            "PK" : "" ,
            "NM_SUBJECT": Subject,
            "GN_CONTENT": "",
            "PK_REF" : ParentKey 
        })
    }).done(function (data) {
        GetDayList();
    });
}


function StartUpdateListen() {


}
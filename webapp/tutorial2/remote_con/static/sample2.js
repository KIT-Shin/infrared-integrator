window.onload = function () {
    let list = document.getElementsByClassName("operation-button");
    for (let i = 0; i < list.length; i++) {
        let button = list[i];
        button.addEventListener("click", function () {
            var xhr = new XMLHttpRequest();
            // ハンドラの登録.
            xhr.onreadystatechange = function () {
                if (xhr.readyState === 4) { //レスポンスを受信したら
                    if (xhr.status == 200 || xhr.status == 304) { //ステータスコードが200または304の場合
                        var data = xhr.responseText;
                        console.log('COMPLETE! :' + data);
                    } else {
                        console.log('Failed. HttpStatus: ' + xhr.statusText);
                    }
                }
            };
            let p = button.parentElement;
            let theta = p.getElementsByClassName("theta")[0].value;
            let phi = p.getElementsByClassName("phi")[0].value;
            let data = p.getElementsByClassName("data")[0].value;
            xhr.open('GET', 'http://localhost:5000/api/operation?theta=' + theta + "&phi=" + phi + "&data=" + data);
            xhr.send();
        });
    }
}

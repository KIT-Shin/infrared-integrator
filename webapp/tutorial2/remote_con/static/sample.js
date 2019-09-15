function rotate(theta, phi) {
    var xhr = new XMLHttpRequest();
    xhr.open('GET', 'http://localhost:5000/api/rotate?theta=' + theta + "&phi=" + phi);
    xhr.send();
}
window.onload = function () {
    let theta = document.getElementById("id_vertical_angle");
    let phi = document.getElementById("id_horizontal_angle");
    let data = document.getElementById("id_infrared");
    theta.onchange = function () {
        if (theta.value < 0) theta.value = 0;
        if (theta.value > 180) theta.value = 180;
        rotate(theta.value, phi.value);
    }
    phi.onchange = function () {
        if (phi.value < 0) phi.value = 0;
        if (phi.value > 180) phi.value = 180;
        rotate(theta.value, phi.value);
    }
    data.setAttribute("disabled", "");
    let doing = false;
    data.parentElement.onclick = function () {
        console.log("clicked")
        if (doing) return;
        doing = true;
        let current = data.value;
        data.value = "リモコンの出力を読み取ります"
        var xhr = new XMLHttpRequest();
        // ハンドラの登録.
        xhr.onreadystatechange = function () {
            if (xhr.readyState === 4) { //レスポンスを受信したら
                console.log(JSON.parse(xhr.responseText))
                if ((xhr.status == 200 || xhr.status == 304) && JSON.parse(xhr.responseText).status == "Success") { //ステータスコードが200または304の場合
                    var d = JSON.parse(xhr.responseText).value;
                    console.log('COMPLETE! :' + d);
                    current = d;
                } else {
                    console.log('Failed. HttpStatus: ' + xhr.statusText);
                }
                data.value = current;
                doing = false;
            }
        };
        xhr.open('GET', 'http://localhost:5000/api/record');
        xhr.send();
    }
}
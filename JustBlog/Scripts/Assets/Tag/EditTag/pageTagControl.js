$(function () {
    //изменял ли пользователь данные формы
    let dataChanged = false;
    $("#btnShowPosts").click(function (e) {
        let empty = $("#postsTarget").has("div");
        let contains = $("#postsTarget").has("*");

        if (contains.length <= 0) {
            $.ajax({
                method: "POST",
                cache: false,
                async: true,
                dataType: "json",
                url: _getRalatedPosts,
                success: function (jsonStr) {
                    let posts = JSON.parse(jsonStr);
                    var table = '<table class="table">';
                    for (var i = 0; i < posts.length; i++) {

                        var tableRow = '<tr>' +
                            '<td>' + posts[i].Id + '</td>' +
                            '<td>' + posts[i].Title + '</td>' +
                            '<td>' + '<input type="checkbox" name="' + posts[i].Title + '" value="' + posts[i].Id + '" checked> <td>' +
                            '</tr>';
                        table += tableRow;
                    }
                    table += '</table>';
                    $('#postsTarget').html(table);
                },
                error: function (err) {
                    $('#postsTarget').html("<div>Не удалось загрузить посты</div>");
                }
            });
        }
    });

    $("#submittedForm").submit(function (e) {
        clickedOnce = true;
        e.preventDefault();
        //to the implementation in partial view reload the validators
        console.log($("#submittedForm"));
        var _form = $("#submittedForm")
            .removeData("validator") /* added by the raw jquery.validate plugin */
            .removeData("unobtrusiveValidation");/* added by the jquery unobtrusive plugin */
        var _formValue = $(this)[0];

        $.validator.unobtrusive.parse(document);
        //_form.validate();

        var isValid = _form.valid();
        //console.log(isValid);
        //console.log("");
        //console.log(_form.data('unobtrusiveValidation'));

        console.log(isValid + " FORM");
        if (isValid) {
            let _data = new FormData(_formValue);

            $.ajax({
                method: "POST",
                processData: false,
                cache: false,
                contentType: false,
                
                url: _postTagAction,
                data: _data,
                success: function (response) {
                    if (response.success === true) {
                        alert(response.responseText);
                        $("#editModal").modal('hide');
                        dataChanged = false;
                        //Перезагружается страницу
                        //location.reload();

                        $.ajax({
                            method: "GET",
                            url: _successedTags,
                            dataType: "html",
                            success: function (response) {
                                //update partly
                                $("#tableBody").empty().html(response);
                            },
                            error: function () {
                                //update fully
                                location.reload();
                            }
                        });
                    } else {
                        // DoSomethingElse()
                        alert(response.responseText);
                    }


                },
                error: function (jqXHR) {
                    alert("Что-то пошло не так...");
                }
            })
                .done(function (data, textStatus, jqXHR) {
                    //DoSomethingUsual()
                });
        }
    });


    //проверка на внесение изменений в данные формы
    $("input, select").change(function () {
        dataChanged = true;
    });

    //подтверждение закрытие модальной формы
    $("#btnClose1, #btnClose2").click(function (e) {
        e.preventDefault();
        //debugger;
        if (dataChanged) {
            var closeConfirmed = confirm("Были внесены изменения. \nЗакрыть окно?");
            if (closeConfirmed) {
                $("#editModal").modal('hide');
                dataChanged = false;
                $("#target").empty();
            }
        }
        else {
            $("#editModal").modal('hide');
            dataChanged = false;
            $("#target").empty();
        }
        //$("#target").empty();
    });
});

function IncludePosts(data) {
    let posts = JSON.parse(data);
    var table = '<table class="table">';
    for (var i = 0; i < posts.length; i++) {

        var tableRow = '<tr>' +
            '<td>' + posts[i].Id + '</td>' +
            '<td>' + posts[i].Title + '</td>' +
            '<td>' + '<input type="checkbox" name="' + posts[i].Title + '" value="' + posts[i].Id + '" checked> <td>' +
            '</tr>';
        table += tableRow;
    }
    table += '</table>';
    $('#postsTarget').html(table);
}
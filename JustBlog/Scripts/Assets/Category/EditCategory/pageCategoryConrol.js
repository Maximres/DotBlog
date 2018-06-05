$(function () {
    let dataChanged = false;

    $("#btnShowPosts").click(function (e) {
        if ($("#postsTarget").has("#")) {
            //TODO: inject posts
        }
    });

    $("#submittedForm").submit(function (e) {
        clickedOnce = true;
        e.preventDefault();
        //to the implementation in partial view reload the validators
        var _form = $("#submittedForm")
            .removeData("validator") /* added by the raw jquery.validate plugin */
            .removeData("unobtrusiveValidation");/* added by the jquery unobtrusive plugin */
        var _formValue = $(this)[0];

        $.validator.unobtrusive.parse(_form);

        var isValid = _form.valid();
        console.log(isValid);
        console.log("");
        console.log(_form.data('unobtrusiveValidation'));


        if (isValid) {
            let _data = new FormData(_formValue);

            $.ajax({
                method: "POST",
                processData: false,
                cache: false,
                contentType: false,
                url: _postCategory,
                data: _data,
                success: function (response) {
                    if (response.success === true) {
                        alert(response.responseText);
                        $("#editModal").modal('hide');
                        dataChanged = false;
                        //reload page
                        //location.reload();

                        $.ajax({
                            method: "GET",
                            url: _getCategories,
                            dataType: "html",
                            //update partly
                            success: function (response) {
                                $("#tableBody").empty().html(response);
                            },
                            //update fully
                            error: function () {
                                location.reload();
                            }
                        });

                    } else {
                        // DoSomethingElse()
                        alert(response.responseText);
                    }


                },
                error: function (jqXHR) {
                    alert("Что-то пошло не так...\nНеудалось отправить форму");
                }
            })
                .done(function (data, textStatus, jqXHR) {

                });
        }
    });

    $("input, select").change(function () {
        dataChanged = true;
    });

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
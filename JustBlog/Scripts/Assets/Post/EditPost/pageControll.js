
$('textarea#froala-editor').on('froalaEditor.file.uploaded', function (e, editor, response) {
    console.log(e);
    console.log(editor);
    console.log(response);
});

$("textarea#froala-editor").on('froalaEditor.image.inserted', function (e, editor, $img, response) {
    console.log(e);
    console.log(editor);
    console.log($img);
    console.log(response);
});

$(function () {
    //изменял ли пользователь данные формы
    let dataChanged = false;


    if (!$('textarea#froala-editor').data('froala.editor')) {
        $('textarea#froala-editor').froalaEditor({
            toolbarButtons: ['bold', 'italic', 'underline', 'strikeThrough', 'subscript', 'superscript', '|', 'fontSize', '|', 'paragraphFormat', 'align', 'formatOL', 'formatUL', 'outdent', 'indent', 'quote', '-', 'insertLink', 'insertImage', 'insertFile', '|', 'insertHR', 'selectAll', 'clearFormatting', '|', 'undo', 'redo'],
            imageUploadParam: 'image_param',
            //declarated in .cshtml
            imageUploadURL: _getImage,
            imageUploadParams: { id: 'my_editor' },
            imageUploadMethod: 'POST',
            imageMaxSize: 5 * 1024 * 1024,
            imageAllowedTypes: ['jpeg', 'jpg', 'png']
        }).on('froalaEditor.image.beforeUpload', function (e, editor, images) {
            // Return false if you want to stop the image upload.
            console.log(images);
            console.log();
        })
            .on('froalaEditor.image.uploaded', function (e, editor, response) {
                // Image was uploaded to the server.
                console.log(responce);
                console.log();
            })
            .on('froalaEditor.image.inserted', function (e, editor, $img, response) {
                // Image was inserted in the editor.
                console.log($img);
                console.log();
            })
            .on('froalaEditor.image.replaced', function (e, editor, $img, response) {
                // Image was replaced in the editor.
            }).on('froalaEditor.image.error', function (e, editor, error, response) {
                // Bad link.
                // if (error.code === 1) { }

                // No link in upload response.
                //else if (error.code === 2) { }

                // Error during image upload.
                //else if (error.code == 3) {
                //   console.log(error);
                //}

                // Parsing response failed.
                //else if (error.code == 4) { }

                // Image too text-large.
                //else if (error.code == 5) { }

                // Invalid image type.
                //else if (error.code == 6) { }

                // Image can be uploaded only to same domain in IE 8 and IE 9.
                //else if (error.code == 7) { }

                // Response contains the original server response to the request if available.
            });
    }


    //установка текущей даты свойству "дата изменения"
    document.getElementById("datePicker").valueAsDate = new Date();

    //var tags = _tags;

    var input3 = document.querySelector('input[name=tags]'),
        tagify3 = new Tagify(input3, {
            delimiters: ", ",
            suggestionsMinChars: 1,
            maxTags: 7,
            blacklist: ["fuck", "shit", "pussy"],
            enforceWhitelist: true,
            //declarated in .cshtml
            whitelist: _tags
        });


    $('#CategoryListId').val(_selectedIndex).change();
    $("#submittedForm").submit(function (e) {
        clickedOnce = true;
        e.preventDefault();
        //to the implementation in partial view reload the validators
        var _form = $("#submittedForm")
            .removeData("validator") /* added by the raw jquery.validate plugin */
            .removeData("unobtrusiveValidation");/* added by the jquery unobtrusive plugin */
        $.validator.unobtrusive.parse(_form);
        _form.validate();

        var _formValue = $(this)[0];

        var isValid = _form.valid();


        if (isValid) {
            let _data = new FormData(_formValue);

            $.ajax({
                method: "POST",
                processData: false,
                cache: false,
                contentType: false,
                //declarated in .cshtml
                url: _postPostAction,
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
                            //declarated in .cshtml
                            url: _getPostsData,
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

function InitEditor() {
    if (!$('#froala-editor').data('froala.editor')) {
        $('#froala-editor').froalaEditor();
    }
}

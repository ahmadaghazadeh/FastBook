/* LOADER */
jQuery(document).ready(function () {
        // Culture Select Box
        $("select").change(function () {
                $("*").blur();
        });

        $("#langSelector").change(function () {
                $(this).parents("form").submit(); // post form
        });

        Dropzone.options.dropzoneForm = {
            acceptedFiles: "image/*",
            init: function () {
                var thisDropzone = this;


                $.getJSON("/home/GetAttachments/").done(function (data) {
                    if (data.Data != '') {

                        $.each(data.Data, function (index, item) {
                            //// Create the mock file:
                            var mockFile = {
                                name: item.AttachmentID,
                                size: 12345
                            };

                            // Call the default addedfile event handler
                            thisDropzone.emit("addedfile", mockFile);

                            // And optionally show the thumbnail of the file:
                            thisDropzone.emit("thumbnail", mockFile, item.Path);

                            // If you use the maxFiles option, make sure you adjust it to the
                            // correct amount:
                            //var existingFileCount = 1; // The number of files already uploaded
                            //myDropzone.options.maxFiles = myDropzone.options.maxFiles - existingFileCount;
                        });
                    }

                });


            }
        };

    });



/* LOADER */
jQuery(document).ready(function () {
        // Culture Select Box
        $("select").change(function () {
                $("*").blur();
        });

        $("#langSelector").change(function () {
                $(this).parents("form").submit(); // post form
            });

    });
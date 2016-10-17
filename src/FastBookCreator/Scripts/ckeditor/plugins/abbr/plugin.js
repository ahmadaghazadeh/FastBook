﻿CKEDITOR.plugins.add('abbr', {

    // Register the icons.
    icons: 'abbr',

    // The plugin initialization logic goes inside this method.
    init: function (editor) {

        // Define an editor command that opens our dialog window.
        editor.addCommand('abbr', new CKEDITOR.dialogCommand('abbrDialog'));

        // Create a toolbar button that executes the above command.
        editor.ui.addButton('Abbr', {

            // The text part of the button (if available) and the tooltip.
            label: 'Insert Abbreviation',

            // The command to execute on click.
            command: 'abbr',

            // The button placement in the toolbar (toolbar group name).
            toolbar: 'insert'
        });

        // Register our dialog file -- this.path is the plugin folder path.
        CKEDITOR.dialog.add('abbrDialog', this.path + 'dialogs/abbr.js');
    }
});
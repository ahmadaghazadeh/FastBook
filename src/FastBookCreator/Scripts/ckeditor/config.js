/**
 * @license Copyright (c) 2003-2016, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
    config.language = 'fa';
    
    config.toolbarGroups = [
		{ name: 'document', groups: ['mode', 'document', 'doctools'] },
		{ name: 'clipboard', groups: ['clipboard', 'undo'] },
		{ name: 'editing', groups: ['find', 'selection',  'editing'] },
		//{ name: 'forms', groups: ['forms'] },
		
		{ name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
       
		{ name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi', 'paragraph'] },
		{ name: 'links', groups: ['links'] },
		{ name: 'insert', groups: ['insert'] },
	 
		{ name: 'styles', groups: ['styles'] },
		{ name: 'colors', groups: ['colors'] },
		{ name: 'tools', groups: ['tools'] },
		{ name: 'others', groups: ['others'] }
    ];

    config.removeButtons = 'NewPage,Print,Templates,Radio,Image,Flash';
    config.extraPlugins = 'fontawesome,codesnippet';
    config.contentsCss = '../Scripts/ckeditor/plugins/fontawesome/font-awesome/css/font-awesome.min.css';
    config.allowedContent = true; 
    
    
};


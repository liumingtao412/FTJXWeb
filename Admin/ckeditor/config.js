/*
Copyright (c) 2003-2010, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function( config )
{
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
	// config.uiColor = '#AADC6E';
	
	
	config.language = "zh-cn";
	config.skin = "v2";
	config.width = "100%";
	config.resize_enabled = false;
	config.toolbar =
	[
	['Source', '-'],
	['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', ],
	['Undo', 'Redo', '-', 'Find', 'Replace', '-', 'SelectAll', 'RemoveFormat'],
	['Image', 'Table', 'HorizontalRule','Flash', 'Smiley', 'SpecialChar', 'PageBreak'],
	'/',
	['Bold', 'Italic', 'Underline', '-', 'Subscript', 'Superscript'],
	['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', 'Blockquote'],
	['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
	['Link', 'Unlink', 'Anchor'],
	'/',
	['Format', 'Font', 'FontSize'],
	['TextColor', 'BGColor'],
	['Maximize', 'ShowBlocks']
	];
};

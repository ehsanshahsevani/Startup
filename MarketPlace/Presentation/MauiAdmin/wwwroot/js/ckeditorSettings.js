function SetEditor(id) {

	// code editor 
	CKEDITOR.replace(id, {
		// Load the German interface. 
		language: 'fa'
	});

}

function GetValue(id) {

	let value = CKEDITOR.instances[id].getData();
	return value;

}

function SetValue(id, value) {

	CKEDITOR.instances[id].setData(value);

}
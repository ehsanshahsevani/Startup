function SetEditor(id) {
	
	CKEDITOR.replace(id, {
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
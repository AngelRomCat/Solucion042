function miAjax(param) {
	var miTabla = "";
	var miTabla01 =
		"   <table>" +
		"        <thead>" +
		"           <tr>" +
		"               <th>EmployeeID</th>" +
		"               <th>FirstName</th>" +
		"               <th>LastName</th>" +
		"               <th>Detail</th>" +
		"           </tr>" +
		"        </thead>" +
		"        <tbody>";
	var miTabla02 = "";
	var miTabla03 =
		"       </tbody>" +
		"   </table>";

	var miTablaDetalle = "";
	const opciones = {
		method: 'GET'
	}
	//[{"EmployeeID":"1","LastName":"Davolio","FirstName":"Nancy","Title":"Sales Representative","TitleOfCourtesy":"Ms.","BirthDate":"1948-12-08 00:00:00","HireDate":"1992-05-01 00:00:00","Address":"507 - 20th Ave. E.Apt. 2A","City":"Seattle","Region":"WA","PostalCode":"98122","Country":"USA","HomePhone":"(206) 555-9857","Extension":"5467","Photo":null,"Notes":"Education includes a BA in psychology from Colorado State University in 1970. She also completed \"The Art of the Cold Call.\" Nancy is a member of Toastmasters International.","ReportsTo":"2","PhotoPath":"http:\/\/accweb\/emmployees\/davolio.bmp","Salary":"2954.55"},
	if (param == null || param == 0) {
		//fetch('https://tupelu.es/Ajax_y_Json/ajax_northwind_mysql/ajax_actual_fetch/detail/consulta.php', opciones)
		//fetch('https://localhost:44351/Api/Empleados',opciones)
		fetch('https://localhost:44334/Api/Empleados', opciones)
			.then(respuesta => respuesta.json())
			.then(resultado => {
				resultado.forEach(elemento => {
					miTabla02 = miTabla02 +
						"                    <tr>" +
						"                        <td>" + elemento.EmployeeID + "</td>" +
						"                        <td>" + elemento.FirstName + "</td>" +
						"                        <td>" + elemento.LastName + "</td>" +
						"                        <td><input type=\"button\" value=\"Detalle\" onclick=\"miAjax(" + elemento.EmployeeID + ")\"></td>" +
						"                    </tr>";
				});
				miTabla = miTabla01 + miTabla02 + miTabla03;
				document.getElementById("miTabla").innerHTML = miTabla;
			});
	} else {
		//fetch('https://tupelu.es/Ajax_y_Json/ajax_northwind_mysql/ajax_actual_fetch/detail/consulta.php?id=' + param, opciones)
		//fetch('https://localhost:44351/Api/Empleados/' + param,opciones)
		fetch('https://localhost:44334/Api/Empleados/' + param, opciones)
			.then(respuesta => respuesta.json())
			.then(resultado => {
				//	resultado.forEach(elemento => {            
				miTablaDetalle = miTablaDetalle +
					"   			   <table>" +
					"                    <tr>" +
					"						 <th>EmployeeID</th>" +
					/*		"                        <td>" + elemento.EmployeeID + "</td>" +*/
					/*		"                        <td>" + resultado[0].EmployeeID + "</td>" +*/
					"                        <td>" + resultado.EmployeeID + "</td>" +
					"                    </tr>" +
					"                    <tr>" +
					"						 <th>FirstName</th>" +
					/*		"                        <td>" + elemento.FirstName + "</td>" +*/
					/*		"                        <td>" + resultado[0].FirstName + "</td>" +*/
					"                        <td>" + resultado.FirstName + "</td>" +
					"                    </tr>" +
					"                    <tr>" +
					"						 <th>LastName</th>" +
					/*		"                        <td>" + elemento.LastName + "</td>" +*/
					/*		"                        <td>" + resultado[0].LastName + "</td>" +*/
					"                        <td>" + resultado.LastName + "</td>" +
					"                    </tr>" +
					"                    <tr>" +
					"						 <th>Imagen</th>" +
					/*		"                        <td><img src=\"https://tupelu.es/Ajax_y_Json/ajax_northwind_mysql/ajax_actual_fetch/detail/Images/empleado/" + elemento.EmployeeID + ".jpg\"></td>" +*/
					/*		"                        <td><img src=\"https://tupelu.es/Ajax_y_Json/ajax_northwind_mysql/ajax_actual_fetch/detail/Images/empleado/" + resultado[0].EmployeeID + ".jpg\"></td>" +*/
					"                        <td><img src=\"https://tupelu.es/Ajax_y_Json/ajax_northwind_mysql/ajax_actual_fetch/detail/Images/empleado/" + resultado.EmployeeID + ".jpg\"></td>" +
					"                    </tr>" +
					"   			   </table>";
				//	});
				//miTabla = miTabla01 + miTabla02 + miTabla03;    
				document.getElementById("miTablaDetalle").innerHTML = miTablaDetalle;
			});
	}
}

function miAjaxJquery(param) {
	var miTablaJquery = "";
	var miTablaJquery01 =
		"   <table>" +
		"        <thead>" +
		"           <tr>" +
		"               <th>EmployeeID</th>" +
		"               <th>FirstName</th>" +
		"               <th>LastName</th>" +
		"               <th>Detail</th>" +
		"           </tr>" +
		"        </thead>" +
		"        <tbody>";
	var miTablaJquery02 = "";
	var miTablaJquery03 =
		"       </tbody>" +
		"   </table>";

	var miTablaJqueryDetalle = "";
	const opciones = {
		method: 'GET'
	}
	/*Esta función monta un string con formato html. Con los datos de TODOS LOS REGISTROS DEL JSON
	Y LO DEVUELVE*/
	function manageDataTable(param) {
		// una vez que obtenga el json parseo los resultados:
		var miTablaJquery04 = "";

		$.each(param, function (propName, propVal) {
			miTablaJquery04 = miTablaJquery04 +
				"                    <tr>" +
				"                        <td>" + propVal.EmployeeID + "</td>" +
				"                        <td>" + propVal.FirstName + "</td>" +
				"                        <td>" + propVal.LastName + "</td>" +
				"                        <td><input type=\"button\" value=\"Detalle\" onclick=\"miAjaxJquery(" + propVal.EmployeeID + ")\"></td>" +
				"                    </tr>";
		});

		return miTablaJquery04;
	}

	/*Esta función monta un string con formato html. Con los datos de EL ÚNICO REGISTRO DEL JSON
	Y LO DEVUELVE*/
	function manageDataTableDetail(param) {
		// una vez que obtenga el json parseo los resultados:
		var miTablaJquery05 = "";
		miTablaJquery05 = miTablaJquery05 +
			"   			   <table>" +
			"                    <tr>" +
			"						 <th>EmployeeID</th>" +
			/*		"                        <td>" + elemento.EmployeeID + "</td>" +*/
			/*		"                        <td>" + resultado[0].EmployeeID + "</td>" +*/
			"                        <td>" + param.EmployeeID + "</td>" +
			"                    </tr>" +
			"                    <tr>" +
			"						 <th>FirstName</th>" +
			/*		"                        <td>" + elemento.FirstName + "</td>" +*/
			/*		"                        <td>" + param[0].FirstName + "</td>" +*/
			"                        <td>" + param.FirstName + "</td>" +
			"                    </tr>" +
			"                    <tr>" +
			"						 <th>LastName</th>" +
			/*		"                        <td>" + elemento.LastName + "</td>" +*/
			/*		"                        <td>" + param[0].LastName + "</td>" +*/
			"                        <td>" + param.LastName + "</td>" +
			"                    </tr>" +
			"                    <tr>" +
			"						 <th>Imagen</th>" +
			/*		"                        <td><img src=\"https://tupelu.es/Ajax_y_Json/ajax_northwind_mysql/ajax_actual_fetch/detail/Images/empleado/" + elemento.EmployeeID + ".jpg\"></td>" +*/
			/*		"                        <td><img src=\"https://tupelu.es/Ajax_y_Json/ajax_northwind_mysql/ajax_actual_fetch/detail/Images/empleado/" + param[0].EmployeeID + ".jpg\"></td>" +*/
			"                        <td><img src=\"https://tupelu.es/Ajax_y_Json/ajax_northwind_mysql/ajax_actual_fetch/detail/Images/empleado/" + param.EmployeeID + ".jpg\"></td>" +
			"                    </tr>" +
			"   			   </table>";

		return miTablaJquery05;
	}
	//[{"EmployeeID":"1","LastName":"Davolio","FirstName":"Nancy","Title":"Sales Representative","TitleOfCourtesy":"Ms.","BirthDate":"1948-12-08 00:00:00","HireDate":"1992-05-01 00:00:00","Address":"507 - 20th Ave. E.Apt. 2A","City":"Seattle","Region":"WA","PostalCode":"98122","Country":"USA","HomePhone":"(206) 555-9857","Extension":"5467","Photo":null,"Notes":"Education includes a BA in psychology from Colorado State University in 1970. She also completed \"The Art of the Cold Call.\" Nancy is a member of Toastmasters International.","ReportsTo":"2","PhotoPath":"http:\/\/accweb\/emmployees\/davolio.bmp","Salary":"2954.55"},
	if (param == null || param == 0) {
		//var apiUrl = 'https://tupelu.es/Ajax_y_Json/ajax_northwind_mysql/ajax_actual_fetch/detail/consulta.php';
		//var apiUrl = 'https://localhost:44351/Api/Empleados'';
		var apiUrl = 'https://localhost:44334/Api/Empleados';
		//llamamos a un objeto de la librería jQuery y le pasamos la apiUrl
		$.ajax({
			//parámetros de configuración del objeto Ajax de jQuery
			contentType: 'application/html; charset=utf-8',
			type: 'GET',
			dataType: 'html',
			url: apiUrl,
			async: true
		})
			//el objeto nos muestra lo que le ha devuelto la api como parámetro "res"
			.done(function (res) {
			//convertimos el parámetro "res" en un json
			var objetoRes = JSON.parse(res);
			//pasamos este json a una función manageDataTable() que nos montará la Tabla
				var stringTable = manageDataTable(objetoRes);
			//tomamos el string con la tabla montada y se lo pasamos a la div con id = miTablaJquery
				//versión jQuery
				$('#miTablaJquery').empty().append(stringTable);
				////versión Javascript nativo
				//document.getElementById("miTablaJquery").innerHTML = stringTable;
		}).fail(function (res) {
			console.log(res);

		}).always(function (res) {
			console.log(res);

		});
	} else {
		//var apiUrl = 'https://tupelu.es/Ajax_y_Json/ajax_northwind_mysql/ajax_actual_fetch/detail/consulta.php/'+ param;
		//var apiUrl = 'https://localhost:44351/Api/Empleados/'+ param;
		var apiUrl = 'https://localhost:44334/Api/Empleados/' + param;
		//llamamos a un objeto de la librería jQuery y le pasamos la apiUrl
		$.ajax({
			//parámetros de configuración del objeto Ajax de jQuery
			contentType: 'application/html; charset=utf-8',
			type: 'GET',
			dataType: 'html',
			url: apiUrl,
			async: true

		})
			//el objeto nos muestra lo que le ha devuelto la api como parámetro "res"
			.done(function (res) {
				//convertimos el parámetro "res" en un json
				var objetoRes = JSON.parse(res);
				//pasamos este json a una función manageDataTable() que nos montará la Tabla
				var stringTable = manageDataTableDetail(objetoRes);
				//tomamos el string con la tabla montada y se lo pasamos a la div con id = miTablaJquery
				//versión jQuery
				$('#miTablaJqueryDetalle').empty().append(stringTable);
				////versión Javascript nativo
				//document.getElementById("miTablaJqueryDetalle").innerHTML = stringTable;

			}).fail(function (res) {
				console.log(res);

			}).always(function (res) {
				console.log(res);

			});
	}
}
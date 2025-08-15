function ToastBootstrapShow(id)
{
	const toastLive = document.getElementById(id)
	const toastBootstrap = bootstrap.Toast.getOrCreateInstance(toastLive)
	toastBootstrap.show()
}

function showNemodar(){
	// مقداردهی داده‌های تستی برای نمودار
	var ctx = document.getElementById('dashboardChart').getContext('2d');
	var myChart = new Chart(ctx, {
		type: 'bar', // می‌توانید انواع مختلف را امتحان کنید: 'bar', 'line', 'pie'
		data: {
			labels: ['الکترونیک', 'پوشاک', 'مواد غذایی', 'کتاب‌ها', 'آرایشی'],
			datasets: [{
				label: 'محصولات فعال',
				data: [540, 324, 130, 45, 289],
				backgroundColor: 'rgba(54, 162, 235, 0.6)',
				borderColor: 'rgba(54, 162, 235, 1)',
				borderWidth: 1
			}, {
				label: 'محصولات غیرفعال',
				data: [20, 15, 10, 4, 8],
				backgroundColor: 'rgba(255, 159, 64, 0.6)',
				borderColor: 'rgba(255, 159, 64, 1)',
				borderWidth: 1
			}, {
				label: 'محصولات حذف‌شده',
				data: [5, 2, 0, 1, 3],
				backgroundColor: 'rgba(255, 99, 132, 0.6)',
				borderColor: 'rgba(255, 99, 132, 1)',
				borderWidth: 1
			}]
		},
		options: {
			responsive: true,
			maintainAspectRatio: false,
			plugins: {
				legend: {
					position: 'top',
				},
				tooltip: {
					enabled: true
				}
			},
			scales: {
				x: {
					beginAtZero: true,
				},
				y: {
					beginAtZero: true
				}
			}
		}
	});
}
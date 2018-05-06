$(function () {
	var clickedButton;
	var promoteButtonClickEvent = $('.promote-btn').on('click', function () {
		clickedButton = this;
	});

	var promoteUserSubmitEvent = $('.promote-form').on('submit', function (event) {
		event.preventDefault();

		var url = this.action;
		var data = $(this).serialize();

		$.ajax({
			type: "POST",
			url: url,
			data: data,
			success: function (response) {
				if (response.isPromoted) {
					$(clickedButton).prop('disabled', true);
					$(clickedButton).text('Promoted');
				}
				else {
					alert('Failed to Promote to Admin!');
				}
			}
		});

		var modalToHide = $(this).closest('#exampleModal');

		modalToHide.modal('hide');
	});
});
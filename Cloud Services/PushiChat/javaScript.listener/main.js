(function() {
	var pubnub = PUBNUB({
		subscribe_key: 'sub-c-259e0b9c-8d43-11e5-84ee-0619f8945a4f'
	});

	var colors = ['blue', 'gold', 'red', 'silver'],
	nextColor = 0;

	// Subscribe to a channel

	pubnub.subscribe({
		channel: 'ChatPat',
		message: function(m) {
			console.log(m);
			decorateMessage(m);
		},
		error: function(error) {
			// Handle error here
			console.log(JSON.stringify(error));
		}
	});

	function decorateMessage(message) {
		var container = document.getElementById('content');
		var parts = message.match(/[^:]+/g);
		var paragraph = document.createElement('p');
		var spanIp = document.createElement('span');
		var spanMessage = document.createElement('span');

		spanIp.style.color = getNextColor();
		spanIp.innerHTML = parts[0].trim() + ' : ';
		
		spanMessage.style.color = getNextColor();
		spanMessage.innerHTML = parts[1].trim();

		paragraph.appendChild(spanIp);
		paragraph.appendChild(spanMessage);

		container.appendChild(paragraph);
	}

	function getNextColor() {
		var selected = colors[nextColor];
		nextColor += 1;

		if (nextColor >= colors.length) nextColor = 0;

		return selected;
	}
})();
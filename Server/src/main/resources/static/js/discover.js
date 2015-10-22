var idStr = document.getElementById('id').value;
var idDOM = document.getElementById('id');

var TagButtonStyle = {
	display: 'inline-block',
	border: '1px solid #7f7f7f',
	borderRadius: '5px',
	padding: '4px 10px',
	margin: '0px 6px',
	cursor: 'pointer'
}

var TagButtonStyleActive = {
	backgroundColor: '#1f1f1f',
	color: 'white'
}
jQuery.extend(TagButtonStyleActive, TagButtonStyle);

var TagButton = React.createClass({
	render: function() {
		return (
			<div style={this.props.active ? TagButtonStyleActive : TagButtonStyle} onClick={this.props.tagOnClick}>
				{this.props.tag.content.length > 0 ? this.props.tag.content : "最近"} ({this.props.tag.amount})
			</div>
		);
	}
});

var TagBox = React.createClass({
	getInitialState: function() {
		return {
			tags: [],
			activeIndex: 0
		};
	},
	componentDidMount: function() {
		$.get("/api/" + idStr + "/tags/all", function(result) {
			tags = [];
			for (key in result) {
				tags.push({
					content: key,
					amount: result[key] 
				});
			}
			tags.sort(function(a, b) {
				return (b.key == "" || a.amount < b.amount) ? 1 : -1;
			})
			for (i = 0; i < tags.length; i++) {
				tags[i].index = i;
			}
			if (this.isMounted()) {
				this.setState({
					tags: tags
				});
			}
		}.bind(this));
	},
	tagOnClick: function(tag, event) {
		this.setState({
			activeIndex: tag.index
		});
		var event = document.createEvent('HTMLEvents');
		event.initEvent("tagOnChange", true, true);
		event.eventType = '123';
		event.tag = tag;
		idDOM.dispatchEvent(event);
	},
	render: function() {
		return (
			<div>
				{
					this.state.tags.map(function(tag) {
						return <TagButton active={tag.index==this.state.activeIndex} key={tag.index} tag={tag} tagOnClick={this.tagOnClick.bind(null, tag)} />;
					}.bind(this))
				}
			</div>
		);
	}
});

var ExprStyle = {
	display: 'inline-block',
	margin: '10px',
	padding: '10px',
	width: '100px',
	height: '100px',
	lineHeight: '100px',
	border: '1px dashed',
	verticalAlign: 'middle',
};

var Expr = React.createClass({
	render: function() {
		var expr = this.props.expr;
		var ExprImgStyle = {
			width: '80px',
			height: '80px',
			backgroundImage: 'url(' + "/expr/" + expr.md5 + expr.extension + ')',
			backgroundRepeat: 'no-repeat',
			backgroundPosition: 'center',
			backgroundSize: 'contain',
		};
		return (
			<div style={ExprStyle}>
				<div style={ExprImgStyle}>
					<img src={"/expr/" + expr.md5 + expr.extension} style={{opacity: '0', width: '100%', height: '100%'}} />
				</div>
			</div>
		);
	}
});

var ExprBoxStyle = {
	marginTop: '15px',
}

var ExprBox = React.createClass({
	getInitialState: function() {
		return {
			exprs: [],
		};
	},
	reloadExprs: function(tag) {
		$.get("/api/" + idStr + "/exprs/all?tag=" + tag, function(result) {
			if (this.isMounted()) {
				this.setState({
					exprs: result,
				});
			}
		}.bind(this));
	},
	componentDidMount: function() {
		idDOM.addEventListener('tagOnChange', function(event) {
			console.log(event.tag.content);
			this.reloadExprs(event.tag.content);
		}.bind(this));
		this.reloadExprs("");
	},
	render: function() {
		return (
			<div style={ExprBoxStyle}>
				{
					this.state.exprs.map(function(expr) {
						return <Expr key={expr.id} expr={expr} />;
					}.bind(this))
				}
			</div>
		);
	}
});

var CollectionBox = React.createClass({
	render: function() {
		return (
			<div>
				<TagBox />
				<ExprBox />
			</div>
		);
	}
})

ReactDOM.render(
		<CollectionBox />,
		document.getElementById('tagBox')
);
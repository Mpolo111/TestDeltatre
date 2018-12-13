import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';

interface Product {
	CatalogId: number;
	ProductName: string;
	Description: string;
	Quantity: number;
}

interface CatalogPaginated {
	products: Product[];
	totalPage: number;
}

interface ProductState {
	catalogPaginated: CatalogPaginated;
}

export class ProductList extends React.Component<any, ProductState> {
	constructor(props: any) {
		super(props);
		this.state = {
			catalogPaginated: { products: [], totalPage: 1 }
		};
		fetch('/api/data/GetProducts')
			.then(response => response.json() as Promise<CatalogPaginated>)
			.then(data => {
				this.setState({ catalogPaginated: data });
			});
	}

	render()
	{
		let ps = this.state.catalogPaginated.products
		return (
			<table className='table'>
			<thead>
				<tr>
					<th>CatalogId</th>
					<th>Product Name</th>
					<th>Description</th>
					<th>Quantitiy</th>
				</tr>
			</thead>
			<tbody>
					{ps.map((p) =>
						<tr key={p.CatalogId}>
							<td>{p.ProductName}</td>
							<td>{p.Description}</td>
							<td>{p.Quantity}</td>
					</tr>
				)}
			</tbody>
			</table>
		);
	}
}
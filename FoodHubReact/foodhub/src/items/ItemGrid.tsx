import React from 'react';
import { Card, Col, Row, Button } from 'react-bootstrap';
import { Item } from '../types/item';

interface ItemGridProps {
  items: Item[];
  apiUrl: string;
  onItemDeleted: (itemId: number) => void;
}

const ItemGrid: React.FC<ItemGridProps> = ({ items, apiUrl, onItemDeleted}) => {

  return (
    <div>
      <Row xs={1} sm={2} md={3} lg={4} className="g-4">
        {items.map(item => (
          <Col key={item.ItemId}>
            <Card>
              <Card.Img variant="top" src={`${apiUrl}${item.ImagePath}`} alt={item.Name} />
              <Card.Body>
                <Card.Title>{item.ItemId} {item.Name}</Card.Title>
                <Card.Text>
                  Producer: {item.ProducerName}
                </Card.Text>
                <Card.Text>
                  {item.Description}
                </Card.Text>
                <Card.Text>
                  Energy: {item.Energy} kcal
                </Card.Text>
                <Card.Text>
                  Carbohydrate: {item.Carbohydrate} g
                </Card.Text>
                <Card.Text>
                  Total Fat: {item.TotalFat} g
                </Card.Text>
                <Card.Text>
                  Saturated Fat: {item.SaturatedFat} g
                </Card.Text>
                <Card.Text>
                  Unsatured Fat: {item.UnsaturedFat} g
                </Card.Text>
                <Card.Text>
                  Sugar: {item.Sugar} g
                </Card.Text>
                <Card.Text>
                  Dietary Fiber: {item.DietaryFiber} g
                </Card.Text>
                <Card.Text>
                  Protein: {item.Protein} g
                </Card.Text>
                <div className="d-flex justify-content-between">
                    <Button href={`/itemupdate/${item.ItemId}`} variant="primary">Update</Button>
                    <Button onClick={(event) => onItemDeleted(item.ItemId)} variant="danger">Delete</Button>
                    </div>
              </Card.Body>
            </Card>
          </Col>
        ))}
      </Row>
    </div>
  );
};

export default ItemGrid;
import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Hoteldetails } from './hoteldetails';

describe('Hoteldetails', () => {
  let component: Hoteldetails;
  let fixture: ComponentFixture<Hoteldetails>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Hoteldetails]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Hoteldetails);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

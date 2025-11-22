import { TestBed } from '@angular/core/testing';

import { BookingServies } from './booking-servies';

describe('BookingServies', () => {
  let service: BookingServies;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BookingServies);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

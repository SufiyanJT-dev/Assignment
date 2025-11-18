import { TestBed } from '@angular/core/testing';

import { employeeServies } from './employeeServies';

describe('BookingServies', () => {
  let service: employeeServies;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(employeeServies);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
